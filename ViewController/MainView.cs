/// <summary> 
/// Author:    Ian Argyle
/// Partner:   None
/// Date:      4/12/2020
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Ian Argyle - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Ian Argyle, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// 
///    Represents the GUI for the Agario game.
/// </summary>


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Models;
using NetworkingNS;
using Newtonsoft.Json;

namespace ViewController
{
    public partial class MainView : Form
    {

        /// <summary>
        /// Keep the server in the class so we can use it everywhere
        /// </summary>
        private Preserved_Socket_State server;
        
        /// <summary>
        /// Keep the world in the class
        /// </summary>
        private World world;

        /// <summary>
        /// Represents an object for locking use for the world
        /// </summary>
        private readonly object locker = new object();

        /// <summary>
        /// Constructor, builds the GUI and initializes the world.
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            world = new World();
        }

        /// <summary>
        /// Draws the circles every heatbeat. Called by invalidate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="p"></param>
        private void Draw_Scene(object sender, PaintEventArgs p)
        {
            // If the world isn't null then we are in the game.
            if (!(world.me is null))
            {
                // Check if the mass of the player is 0, if it is the player is dead
                if (world.me.mass == 0)
                {
                    MessageBox.Show("You Died");
                    world.me = null;
                    Death();
                }
                else
                {
                    // Draw some stats on the screen
                    p.Graphics.DrawString($"X: {world.me.location.X.ToString()}", this.Font, Brushes.Black, this.Width - 90, 20);
                    p.Graphics.DrawString($"Y: {world.me.location.Y.ToString()}", this.Font, Brushes.Black, this.Width - 90, 40);
                    p.Graphics.DrawString($"Mass: {world.me.mass.ToString()}", this.Font, Brushes.Black, this.Width - 90, 60);
                    
                    // Invalidate again to maximize fps
                    this.Invalidate();

                    // Lock so we dont corrupt the world data
                    lock (locker)
                    {
                        Brush brush = new SolidBrush(Color.FromArgb(world.me.color));
                        int meSize = (int)Math.Sqrt(world.me.mass / 3.14);
                        // Computer the upper and lower bounds of what we can see on the screen
                        int xLow = (int)(world.me.location.X - (meSize * 40));
                        int xHigh = (int)(world.me.location.X + (meSize * 40));
                        int yLow = (int)(world.me.location.Y - (meSize * 40));
                        int yHigh = (int)(world.me.location.Y + (meSize * 40));
                        int span = xHigh - xLow;

                        // Draw all the circles that are visible in the zoom
                        foreach (KeyValuePair<int, Circle> circle in world.circles)
                        {
                            if (circle.Value.location.X > xLow && circle.Value.location.X < xHigh && circle.Value.location.Y > yLow && circle.Value.location.Y < yHigh)
                            {
                                Brush brushother = new SolidBrush(Color.FromArgb(circle.Value.color));
                                int otherSize = (int)Math.Sqrt(circle.Value.mass / 3.14);
                                p.Graphics.FillEllipse(brushother, new Rectangle((int)(((circle.Value.location.X - xLow) * this.ClientSize.Width) / span) - (otherSize * 2), (int)(((circle.Value.location.Y - yLow) * this.ClientSize.Height) / span) - (otherSize * 2), otherSize * (5000 / this.ClientSize.Width), otherSize * (5000 / this.ClientSize.Height)));
                                p.Graphics.DrawString(circle.Value.name, this.Font, Brushes.Black, (int)(((circle.Value.location.X - xLow) * this.ClientSize.Height) / span), (int)(((circle.Value.location.Y - yLow) * this.ClientSize.Width) / span));
                            }
                        }

                        // Draw the player
                        p.Graphics.FillEllipse(brush, new Rectangle((int)(((world.me.location.X - xLow) * this.ClientSize.Width) / span) - (meSize * 2), (int)(((world.me.location.Y - yLow) * this.ClientSize.Height) / span) - (meSize * 2), meSize * (5000 / this.ClientSize.Width), meSize * (5000 / this.ClientSize.Height)));
                        p.Graphics.DrawString(world.me.name, this.Font, Brushes.Black, (int)(((world.me.location.X - xLow - meSize) * this.ClientSize.Width) / span), (int)(((world.me.location.Y - yLow - meSize) * this.ClientSize.Height) / span));
                    }
                }
            }
        }

        /// <summary>
        /// Called when the player dies, resets the screen
        /// </summary>
        private void Death()
        {
            Debug.WriteLine("Player Died");
            this.PlayerNameLabel.Visible = true;
            this.PlayerNameBox.Visible = true;
            this.ServerLabel.Visible = true;
            this.ServerBox.Visible = true;
            this.ConnectBtn.Visible = true;
        }

        /// <summary>
        /// Connect to the server, make the initial controls invisible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connect(object sender, EventArgs e)
        {
            Debug.WriteLine($"Attempting to connect to server {this.ServerBox.Text}");
            try
            {
                this.server = Networking.Connect_to_Server(Contact_Established, this.ServerBox.Text);
                this.PlayerNameLabel.Visible = false;
                this.PlayerNameBox.Visible = false;
                this.ServerLabel.Visible = false;
                this.ServerBox.Visible = false;
                this.ConnectBtn.Visible = false;
            }
            catch (Exception x)
            {
                Debug.WriteLine("Failed to connect to server");
                MessageBox.Show($"Failed to connect to server\n\n{x.Message}");
            }
        }

        /// <summary>
        /// Phase 0 of the connection protocol, upon connection send the player
        /// name to the server.
        /// </summary>
        /// <param name="obj"></param>
         private void Contact_Established(Preserved_Socket_State obj)
        {
            Debug.WriteLine("Sucessfully connected to server");
            obj.on_data_received_handler = Phase1;
            Networking.Send(obj.socket, this.PlayerNameBox.Text);
            Networking.await_more_data(obj);
        }

        /// <summary>
        /// Phase one of the connection protocol, gets the player circle object from the server
        /// </summary>
        /// <param name="obj"></param>
        private void Phase1(Preserved_Socket_State obj)
        {
            Circle result = JsonConvert.DeserializeObject<Circle>(obj.Message);
            world.me = result;
            // Get ready for the next phase by updating the on data recieved handler
            obj.on_data_received_handler = Phase2;
            Networking.await_more_data(obj);
        }

        /// <summary>
        /// Phase two of the connection protocol, recieves continuous messages from the sever
        /// and updates the game world
        /// </summary>
        /// <param name="obj"></param>
        private void Phase2(Preserved_Socket_State obj)
        {
            try
            {
                Circle circle = JsonConvert.DeserializeObject<Circle>(obj.Message);
                if (circle.type == 2)
                {
                    Trace.WriteLine("Recieved heartbeat from server");
                    // Convert the mouse coordinates to world coordinates and send it to the server
                    int x = (int)world.me.location.X + (Cursor.Position.X - (this.Left + (this.Width / 2)));
                    int y = (int)world.me.location.Y + (Cursor.Position.Y - (this.Top + (this.Height / 2)));
                    Networking.Send(obj.socket, $"(move, {x}, {y})");
                }
                if (circle.id == world.me.id)
                {
                    lock (locker)
                    {
                        world.me = circle;
                    }
                }
                else
                {
                    lock (locker)
                    {
                        world.circles[circle.id] = circle;
                    }
                }
                this.Invalidate();
                if (!obj.Has_More_Data())
                {
                    Networking.await_more_data(obj);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to parse server message: {obj.Message}");
                Networking.await_more_data(obj);
            }
        }

    }
}

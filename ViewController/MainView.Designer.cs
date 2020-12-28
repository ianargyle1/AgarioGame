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
///    GUI builder for the Agario game.
/// </summary>
/// 

using System.Drawing;
using System.Windows.Forms;

namespace ViewController
{
    partial class MainView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 1000);
            this.Text = "Agario";

            this.PlayerNameLabel = new System.Windows.Forms.Label();
            this.PlayerNameLabel.AutoSize = true;
            this.PlayerNameLabel.Name = "PlayerNameLabel";
            this.PlayerNameLabel.Text = "Player Name";

            this.PlayerNameBox = new System.Windows.Forms.TextBox();
            this.PlayerNameBox.Width = 150;
            this.PlayerNameBox.Name = "PlayerNameBox";

            this.ServerLabel = new System.Windows.Forms.Label();
            this.ServerLabel.AutoSize = true;
            this.ServerLabel.Name = "ServerLabel";
            this.ServerLabel.Text = "Server Address";

            this.ServerBox = new System.Windows.Forms.TextBox();
            this.ServerBox.Width = 150;
            this.ServerBox.Name = "ServerBox";

            this.ConnectBtn = new System.Windows.Forms.Button();
            this.ConnectBtn.Width = 80;
            this.ConnectBtn.Height = 30;
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.Click += Connect;


            DoubleBuffered = true;

            // Place everything with relation to the size fo the form, just in case we need to resize later
            this.PlayerNameLabel.Location = new Point((this.Size.Width / 2) - (this.PlayerNameLabel.Size.Width+this.PlayerNameBox.Size.Width)/2, (this.Size.Height / 2) - (this.PlayerNameBox.Height * 2) - this.ConnectBtn.Height - 10);
            this.PlayerNameBox.Location = new Point(this.PlayerNameLabel.Location.X + this.PlayerNameLabel.Size.Width, this.PlayerNameLabel.Location.Y);
            this.ServerLabel.Location = new Point(this.PlayerNameLabel.Location.X, this.PlayerNameLabel.Location.Y + this.PlayerNameLabel.Size.Height + 10);
            this.ServerBox.Location = new Point(this.ServerLabel.Location.X + this.ServerLabel.Size.Width, this.ServerLabel.Location.Y);
            this.ConnectBtn.Location = new Point(this.ServerBox.Location.X+this.ServerBox.Width-this.ConnectBtn.Width, this.ServerBox.Location.Y + this.ServerBox.Height + 10);

            this.Paint += new PaintEventHandler(Draw_Scene);

            this.Controls.Add(PlayerNameLabel);
            this.Controls.Add(PlayerNameBox);
            this.Controls.Add(ServerLabel);
            this.Controls.Add(ServerBox);
            this.Controls.Add(ConnectBtn);
        }

        private Label ServerLabel;
        private Label PlayerNameLabel;
        private TextBox PlayerNameBox;
        private TextBox ServerBox;
        private Button ConnectBtn;
        #endregion
    }
}


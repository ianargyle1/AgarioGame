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
///    Represents the world model for the Agario game.
/// </summary>

using System.Collections.Generic;

namespace Models
{
    public class World
    {

        /// <summary>
        /// Keeps the width of the world
        /// </summary>
        public int width { get; set; }

        /// <summary>
        /// Keeps the height of the world
        /// </summary>
        public int height { get; set; }

        /// <summary>
        /// Keeps the player
        /// </summary>
        public Circle me { get; set; }

        /// <summary>
        /// Keeps all the circles and food
        /// </summary>
        public Dictionary<int, Circle> circles;

        /// <summary>
        /// Contructor, initializes the circles dictionary
        /// </summary>
        public World()
        {
            circles = new Dictionary<int, Circle>();
        }
    }
}

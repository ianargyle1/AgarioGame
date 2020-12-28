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
///    Represents a circle for the Agario game.
/// </summary>


using Newtonsoft.Json;
using System;
using System.Numerics;

namespace Models
{
    public class Circle
    {
        /// <summary>
        /// Keeps the location as a vector
        /// </summary>
        [JsonProperty(PropertyName = "loc")]
        public Vector2 location { get; set; }

        /// <summary>
        /// Keeps the color
        /// </summary>
        [JsonProperty(PropertyName = "argb_color")]
        public int color { get; set; }

        /// <summary>
        /// Keep the ID
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        /// <summary>
        /// Keeps belongs to
        /// </summary>
        [JsonProperty(PropertyName = "belongs_to")]
        public int belongs_to { get; set; }

        /// <summary>
        /// Keeps the type
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public int type { get; set; }

        /// <summary>
        /// Keeps the name of the player (if it is a player)
        /// </summary>
        [JsonProperty(PropertyName = "Name")]
        public String name { get; set; }

        /// <summary>
        /// Keeps the mass
        /// </summary>
        [JsonProperty(PropertyName = "Mass")]
        public double mass { get; set; }
    }
}

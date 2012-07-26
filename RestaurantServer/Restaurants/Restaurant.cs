using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantServer.Restaurants
{
    /// <summary>
    /// Restaurant wrapper.
    /// </summary>
    public class Restaurant
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Restaurant(string name)
        {
            Name = name;
            Menu = new RestaurantMenu();
        }

        public string Name { get; private set; }

        public RestaurantMenu Menu { get; set; }
    }
}

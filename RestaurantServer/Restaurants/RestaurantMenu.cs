using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantServer.Restaurants
{
    /// <summary>
    /// Contains the details of a restaurant menu as a collection of 
    /// menu items.
    /// </summary>
    public class RestaurantMenu
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RestaurantMenu() 
        {
            Items = new Dictionary<string, RestaurantMenuItem>();
        }

        public Dictionary<string, RestaurantMenuItem> Items { get; private set; }

        /// <summary>
        /// An item in a restaurant menu.
        /// </summary>
        public class RestaurantMenuItem
        {
            public RestaurantMenuItem(string name, string description, string price)
            {
                Name = name;

                Description = description;

                Price = price;
            }

            public string Name { get; private set; }

            public string Description { get; private set; }

            public string Price { get; private set; }
        }
    }
}

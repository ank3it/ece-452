using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RestaurantServer.Restaurants;

namespace RestaurantServer
{
    public class RestaurantHandler
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        private RestaurantHandler() 
        {
            Restaurants = new Dictionary<string, Restaurant>();
        }

        /// <summary>
        /// Gets the restaurant which matches the given name.
        /// </summary>
        /// <param name="name">Name of restaurant </param>
        /// <returns></returns>
        public Restaurant GetRestaurant(string name)
        {

            if (name == "Du Ma May")
            {
                name = "Du Ma May Restaurant";
            }

            return Restaurants[name];
        }

        /// <summary>
        /// Add a menu item to a specified resturant's menu.
        /// </summary>
        public void AddMenuItem(string name, string menuItem,
            string description, string price)
        {
            var item =
                new RestaurantMenu.RestaurantMenuItem(menuItem, description, price);

            GetRestaurant(name).Menu.Items.Add(menuItem, item);
        }

        /// <summary>
        /// Singleton instance of this class.
        /// </summary>
        public static RestaurantHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RestaurantHandler();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Collection of restaurants which this handler maintains.
        /// </summary>
        public Dictionary<string, Restaurant> Restaurants { get; private set; }

        private static RestaurantHandler _instance;
    }
}

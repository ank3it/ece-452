using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RestaurantServer.Restaurants;
using RestaurantServer.Contracts;

namespace RestaurantServer.Utilities
{
    public class MessageParser
    {
        public static String Parse(String msg)
        {
            msg = msg.Substring(0, msg.Length - 1);
            string[] msgArray = msg.Split(':');
            string msgType = msgArray[0];
            string msgParameters = msgArray[1];
            string[] msgParametersArray = msgParameters.Split(',');

            switch (msgType)
            {
                case "REQUEST_MENU":
                    string requestedRestaurant = msgParametersArray[0];
                    RestaurantMenu menu = 
                        RestaurantHandler.Instance.GetRestaurant(requestedRestaurant).Menu;

                    //construct string for restaurant menu
                    string restaurantMenuString = string.Empty;
                    foreach (RestaurantMenu.RestaurantMenuItem item in menu.Items.Values)
                    {
                        restaurantMenuString += string.Format("{0},{1},",
                            item.Name,
                            //item.Description,
                            item.Price);
                    }

                    restaurantMenuString = 
                        restaurantMenuString.Substring(0, restaurantMenuString.Length - 1);

                    return "REPLY_MENU:" + restaurantMenuString + "\n";
                case "PLACE_ORDER":
                    string customerName = msgParametersArray[0];
                    string customerAddress = msgParametersArray[1];
                    string customerNumber = msgParametersArray[2];
                    string restaurantName = msgParametersArray[3];

                    Dictionary<string, int> items = new Dictionary<string, int>();

                    for (int i = 4; i < msgParametersArray.Length; i = i + 2)
                    {
                        items.Add(msgParametersArray[i], int.Parse(msgParametersArray[i + 1]));
                    }

                    // place order 
                    Order o = new Order(Program.OrderCount++, items, restaurantName);
                    Customer c = new Customer(customerName, customerAddress, customerNumber);
                    SystemEventPublisher.Instance.PublishNewOrder(o, c);
                    return "\n";
            }

            return null;

        }
    }
}

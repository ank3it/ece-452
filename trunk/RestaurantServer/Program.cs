using System;
using System.Text;
using System.Threading;
using System.ServiceModel;
using System.ServiceModel.Channels;

using RestaurantServer.Contracts;
using RestaurantServer.Utilities;
using System.Collections.Generic;
using RestaurantServer.Restaurants;

namespace RestaurantServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Start event publication service
            SystemLogger.Instance.LogToConsole("Initializing event publication service...");
            ServiceHost host2 = new ServiceHost(typeof(SystemEventPublisher));
            host2.Open();

            SocketHandler sh = new SocketHandler();
            sh.StartListening();
            SystemLogger.Instance.LogToConsole("Socket-listening threads are active.");

            // initialize server objects
            //SMSNotifier.Instance.NewOrder(null, null);
            RestaurantHandler.Instance.Restaurants.Add("Du Ma May Restaurant",
                new Restaurants.Restaurant("Du Ma May Restaurant"));
            RestaurantHandler.Instance.AddMenuItem("Du Ma May Restaurant",
                "Combo #7", "n/a", "10.99");
            RestaurantHandler.Instance.AddMenuItem("Du Ma May Restaurant",
                "Duck Meat with Soy Sauce", "n/a", "10.99");
            RestaurantHandler.Instance.AddMenuItem("Du Ma May Restaurant",
                "Spring Rolls with Fries", "n/a", "5.00");
            RestaurantHandler.Instance.AddMenuItem("Du Ma May Restaurant",
                "Curry Rice with Chicken", "n/a", "8.00");

            SystemLogger.Instance.LogToConsole("Restaurants loaded.");
            SystemLogger.Instance.LogToConsole("System online.");
            Console.WriteLine("\r\nServer hot-keys:");
            Console.WriteLine("(e) - Exit.");
            Console.WriteLine("(v) - View Menus.");
            Console.WriteLine("(p) - Ping SMS Service.");
            while (true)
            {
                
                string key = Console.ReadKey(false).KeyChar.ToString();

                if (key == "e")
                {
                    //break;
                    
                    // create sample order
                    Dictionary<string, int> items = new Dictionary<string,int>();
                    items.Add("Combo #7", 2);
                    Order o = new Order(OrderCount++, items, "Du Ma May Restaurant");

                    SystemEventPublisher.Instance.PublishNewOrder(
                        o, new Customer("Test client", "addr", "6477853904"));
                }
                if (key == "v")
                {
                    SystemLogger.Instance.LogToConsole("Menus requested:");

                    foreach (Restaurant r in RestaurantHandler.Instance.Restaurants.Values)
                    {
                        SystemLogger.Instance.LogToConsole("- " + r.Name);

                        foreach (
                            RestaurantServer.Restaurants.RestaurantMenu.RestaurantMenuItem i 
                                in r.Menu.Items.Values)
                        {
                            SystemLogger.Instance.LogToConsole(string.Format("{0} - {1}", 
                                i.Name,
                                i.Price));
                        }
                    }
                }
                if (key == "p")
                {
                    SMSNotifier.Instance.PingTextPort();
                }
            }
            host2.Close();
        }

        public static bool ExitFlag = false;

        public static int OrderCount = 0;
    }
}

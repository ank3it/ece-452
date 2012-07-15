using System;
using System.Text;
using System.Threading;
using System.ServiceModel;
using System.ServiceModel.Channels;

using RestaurantServer.Contracts;
using RestaurantServer.Utilities;
using System.Collections.Generic;

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
            RestaurantHandler.Instance.Restaurants.Add("Chink Foo",
                new Restaurants.Restaurant("Chink Foo"));
            RestaurantHandler.Instance.AddMenuItem("Chink Foo",
                "Combo #7", "A delicious plate with ASIAN sauce.", "10.99");
            RestaurantHandler.Instance.AddMenuItem("Chink Foo",
                "Dog Meat with Soy Sauce", "Golden retriever roasted with soy sauce laid on a bed of rice.", "10.99");
            RestaurantHandler.Instance.AddMenuItem("Chink Foo",
                "Sudhir's mom.", "Disappointment.", "50.00");

            SystemLogger.Instance.LogToConsole("Restaurants loaded.");
            SystemLogger.Instance.LogToConsole("System online.");
            Console.WriteLine("\r\nServer hot-keys:");
            Console.WriteLine("(e) - Exit.");
            while (true)
            {
                
                string key = Console.ReadKey(false).KeyChar.ToString();

                if (key == "e")
                {
                    break;
                    /*
                    // create sample order
                    Dictionary<string, int> items = new Dictionary<string,int>();
                    items.Add("Burger", 2);
                    Order o = new Order(OrderCount++, items);

                    SystemEventPublisher.Instance.PublishNewOrder(o, new Customer("Test client", "", ""));*/
                }
                //SystemEventPublisher.Instance.PublishEvent(new Event(Console.ReadLine(), DateTime.Now));
            }
            host2.Close();
        }

        public static bool ExitFlag = false;

        public static int OrderCount = 0;
    }
}

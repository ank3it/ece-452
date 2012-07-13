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

            SystemLogger.Instance.LogToConsole("System online.");
            Console.WriteLine("\r\nServer hot-keys:");
            Console.WriteLine("(n) - Create New Sample Order");
            while (true)
            {
                string key = Console.ReadKey(false).KeyChar.ToString();

                if (key == "n")
                {
                    // create sample order
                    Dictionary<string, int> items = new Dictionary<string,int>();
                    items.Add("Burger", 2);
                    Order o = new Order(OrderCount++, items);

                    SystemEventPublisher.Instance.PublishNewOrder(o);
                }
                //SystemEventPublisher.Instance.PublishEvent(new Event(Console.ReadLine(), DateTime.Now));
            }
            host2.Close();
        }

        public static bool ExitFlag = false;

        public static int OrderCount = 0;
    }
}

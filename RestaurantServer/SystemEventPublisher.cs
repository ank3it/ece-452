using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

using RestaurantServer.Contracts;
using RestaurantServer.Utilities;
using RestaurantServer.Restaurants;

namespace RestaurantServer
{
    /// <summary>
    /// Publishes system wide events to any registered clients.
    /// </summary>
    public class SystemEventPublisher : IEventPublisher
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        private SystemEventPublisher() { }

        /// <summary>
        /// Publishes an event to all subscribed clients.
        /// </summary>
        /// <param name="e">The event to publish.</param>
        public void PublishEvent(Event e)
        {
            subscribers.ForEach(x => x.OnPublishedEvent(e));
        }

        /// <summary>
        /// Notify clients of new order and place new order into collection.
        /// </summary>
        /// <param name="o"></param>
        public void PublishNewOrder(Order o, Customer c)
        {
            subscribers.ForEach(x => x.NewOrder(o, c));
            OrderCollectionHandler.Instance.AddOrder(o);
        }

        /// <summary>
        /// Get outstanding orders from clients.
        /// </summary>
        /// <returns></returns>
        public void GetOrders()
        {
            // orders will get ssend to message
            PublishEvent(new Event("REQUEST_ORDERS_TO_SERVER", DateTime.Now));
        }

        /// <summary>
        /// Subscribes any non-wcf listener.
        /// </summary>
        /// <param name="listener"></param>
        public void SubscribeListener(IEventCallback listener)
        {
            if (!subscribers.Contains(listener))
                subscribers.Add(listener);
        }

        /// <summary>
        /// Subscribes a client.
        /// </summary>
        public bool Subscribe()
        {
            try
            {
                IEventCallback callback = OperationContext.Current.GetCallbackChannel<IEventCallback>();
                if (!subscribers.Contains(callback))
                    subscribers.Add(callback);

                SystemLogger.Instance.LogToConsole("New client subscribed.");
                return true;
            }
            catch(Exception e)
            {
                SystemLogger.Instance.LogToConsole("Could not subscribe client: " + e.ToString());
                return false;
            }
        }

        /// <summary>
        /// Unsubscribes a client.
        /// </summary>
        public bool Unsubscribe()
        {
            try
            {
                IEventCallback callback = OperationContext.Current.GetCallbackChannel<IEventCallback>();
                if (subscribers.Contains(callback))
                    subscribers.Remove(callback);

                SystemLogger.Instance.LogToConsole("Client unsubscribed.");
                return true;
            }
            catch
            {
                SystemLogger.Instance.LogToConsole("Could not unsubscribe client.");
                return false;
            }
        }

        /// <summary>
        /// Get a message from the client.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string SendMessage(string message)
        {
            SystemLogger.Instance.LogToConsole("Message received: " + message);
            if (!message.Contains("UPDATE_TIME"))
            {
                // assume all messages are for menu changes
                string[] msgObjs = message.Split(',');
                string restaurantName = msgObjs[0];
                RestaurantMenu menu = new RestaurantMenu();
                for (int i = 1; i < msgObjs.Length; i = i + 2)
                {
                    menu.Add(msgObjs[i], msgObjs[i + 1]);
                }

                RestaurantHandler.Instance.GetRestaurant(restaurantName).Menu = menu;

                SystemLogger.Instance.LogToConsole("Menu updated for '"
                    + restaurantName + "'.");

                return "Message received";
            }
            else
            {
                message = message.Replace("UPDATE_TIME:", string.Empty);
                string id = message.Split(',')[0];
                string time = message.Split(',')[1];
                // SMS - delivery boy
                // update clients
                OrderUpdate o = new OrderUpdate(int.Parse(id), decimal.Parse(time));
                SMSNotifier.Instance.OrderCompletionTimePushed(o);
                subscribers.ForEach(x => x.OrderCompletionTimePushed(o));

                return "Message received";
            }
        }

        /// <summary>
        /// Singleton instance of this class.
        /// </summary>
        public static SystemEventPublisher Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SystemEventPublisher();

                return _instance;
            }
        }

        private static readonly List<IEventCallback> subscribers = new List<IEventCallback>();

        private static SystemEventPublisher _instance;
    }
}

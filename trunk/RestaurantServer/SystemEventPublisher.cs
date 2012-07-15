using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

using RestaurantServer.Contracts;
using RestaurantServer.Utilities;

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

            return "Message received";
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

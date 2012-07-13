using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestaurantServer.Contracts;

namespace RestaurantServer
{
    /// <summary>
    /// Handles operations to a collection of order entities.
    /// </summary>
    public class OrderCollectionHandler
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        private OrderCollectionHandler() { }

        /// <summary>
        /// Adds an order to the collection.
        /// </summary>
        /// <param name="o">Order to add.</param>
        public void AddOrder(Order o)
        {
            _orders.Add(o.ID, o);
        }

        /// <summary>
        /// Removes an order from the collection.
        /// </summary>
        /// <param name="o">Order to remove.</param>
        /// <returns>True if successful.</returns>
        public bool RemoveOrder(Order o)
        {
            if(_orders.ContainsKey(o.ID))
            {
                _orders.Remove(o.ID);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Replaces an order in the collection.
        /// </summary>
        /// <param name="orderId">ID of order to replace.</param>
        /// <param name="o">Order for the replacement.</param>
        /// <returns>True if successful.</returns>
        public bool ReplaceOrder(int orderId, Order o)
        {
            if (RemoveOrder(o))
            {
                AddOrder(o);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Singleton instance of this order collection handler.
        /// </summary>
        public static OrderCollectionHandler Instance 
        {
            get
            {
                if (_instance == null)
                    _instance = new OrderCollectionHandler();

                return _instance;
            }
        }

        private static OrderCollectionHandler _instance;

        private Dictionary<int, Order> _orders = new Dictionary<int, Order>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RestaurantClient_Chef.EventPublisher;

namespace RestaurantClient_Chef
{
    /// <summary>
    /// A global store of WCF service clients between the physical security system 
    /// and this client.
    /// </summary>
    public class GlobalServiceClients
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        private GlobalServiceClients() 
        {
            //PublisherClient = new EventPublisherClient(
        }

        /// <summary>
        /// Client for the event publication service.
        /// </summary>
        public EventPublisherClient PublisherClient { get; set; }


        /// <summary>
        /// Singleton instance of this type.
        /// </summary>
        public static GlobalServiceClients Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GlobalServiceClients();
                }

                return _instance;
            }
        }

        private static GlobalServiceClients _instance;
    }
}

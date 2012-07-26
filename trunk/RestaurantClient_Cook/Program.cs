using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ServiceModel;

using RestaurantClient_Cook.EventPublisher;

namespace RestaurantClient_Cook
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SimpleMessageUI ui = new SimpleMessageUI();
            WCFClient = new EventPublisherClient(new InstanceContext(ui));
            WCFClient.Subscribe();
            Application.Run(ui);
        }

        public static EventPublisherClient WCFClient { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using RestaurantServer.Contracts;
using RestaurantServer;

namespace RestaurantServer.Utilities
{
    /// <summary>
    /// Pushes any oncoming events to an online SMS service
    /// </summary>
    public class SMSNotifier : IEventCallback
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        private SMSNotifier() 
        {
            Enabled = true;
        }

        public void OnPublishedEvent(Event e)
        {
        }

        /// <summary>
        /// Ping TextPort SMS Service.
        /// </summary>
        public void PingTextPort()
        {
            string pingResult = null;

            try
            {
                using (TextPortSMSService.TextPortSMSClient textPortSvc 
                    = new TextPortSMSService.TextPortSMSClient())
                {
                    pingResult = textPortSvc.Ping();
                }
            }
            catch (Exception ex)
            {
                pingResult = "Ping request failed. Message: " + ex.Message;
            }

            SystemLogger.Instance.LogToConsole(pingResult);
        }


        public bool Enabled { get; set; }

        /// <summary>
        /// Singleton instance of this type.
        /// </summary>
        public static SMSNotifier Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SMSNotifier();

                return _instance;
            }
        }

        private static SMSNotifier _instance;


        public void OrderCompletionTimePushed(OrderUpdate update)
        {
            if (Enabled)
            {
                using (TextPortSMSService.TextPortSMSClient textPortSvc = new TextPortSMSService.TextPortSMSClient())
                {
                    // Use your TextPort.com login and password here.
                    TextPortSMSService.TextPortSMSMessages messageList = new TextPortSMSService.TextPortSMSMessages()
                    {
                        UserName = "ece355text",
                        Password = "ece355text",
                        Messages = new List<TextPortSMSService.TextPortSMSMessage>()
                    };

                    // Add first message
                    messageList.Messages.Add(new TextPortSMSService.TextPortSMSMessage()
                    {
                        CountryCode = "CA",
                        MobileNumber = "2269722563",
                        MessageText = string.Format("New order received. Pick-up time: {0}",
                            DateTime.Now.AddMinutes(
                                double.Parse(update.Minutes.ToString())).ToString())

                    });

                    // Add second message
                    messageList.Messages.Add(new TextPortSMSService.TextPortSMSMessage()
                    {
                        CountryCode = "CA",
                        MobileNumber = "5197295571",
                        MessageText = string.Format("Your order will be ready shortly...",
                            DateTime.Now.AddMinutes(
                                double.Parse(update.Minutes.ToString())).ToString())

                    });

                    try
                    {
                        // Submit the messages to the TextPort service
                        TextPortSMSService.TextPortSMSResponses responseList =
                            textPortSvc.SendMessages(messageList);

                        foreach (TextPortSMSService.TextPortSMSResponse resp in responseList.Responses)
                        {
                            SystemLogger.Instance.LogToConsole(
                                "Message #" + resp.ItemNumber.ToString() + " submitted with result " + resp.Result);
                        }
                    }
                    catch { }
                }
            }
        }

        void IEventCallback.OnPublishedEvent(Event e)
        {
            //throw new NotImplementedException();
        }

        public void  NewOrder(Order update, Customer customer)
        {
 	        //throw new NotImplementedException();
        }


        void IEventCallback.NewOrder(Order update, Customer customer)
        {
            //throw new NotImplementedException();
        }
    }
}

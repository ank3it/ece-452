using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RestaurantClient_Chef.EventPublisher;

namespace RestaurantClient_Chef
{
    public partial class SimpleMessageUI : Form, IEventPublisherCallback
    {
        public SimpleMessageUI()
        {
            InitializeComponent();
        }

        private void SimpleMessageUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.WCFClient.Unsubscribe();
            this.Close();
        }

        private void button_Send_Click(object sender, EventArgs e)
        {
            if (!_subscribed)
            {
                //subscribe
                Program.WCFClient.Subscribe();
                _subscribed = true;
            }

            Program.WCFClient.SendMessage(textBox1.Text);
        }

        public void OnPublishedEvent(Event e)
        {
            richTextBox_Status.Text += e.EventDescription;
        }

        private bool _subscribed = false;


        public void OrderCompletionTimePushed(OrderUpdate update)
        {
            throw new NotImplementedException();
        }

        public void NewOrder(Order order, Customer customer)
        {
            Orders.Add(customer, order);

            UpdateTable();
        }

        private void UpdateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID"));

        }

        private Dictionary<Customer, Order> Orders = new Dictionary<Customer, Order>();
    }
}

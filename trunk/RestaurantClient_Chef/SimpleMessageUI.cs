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
            _orders = new DataTable();
            _orders.Columns.Add(new DataColumn("ID"));
            _orders.Columns.Add(new DataColumn("Total Price"));
            _orders.Columns.Add(new DataColumn("Customer Name"));
            _orders.Columns.Add(new DataColumn("Customer Address"));
            _orders.Columns.Add(new DataColumn("Customer Number"));
            _orders.Columns.Add(new DataColumn("Order Status"));

            _orderDetails = new DataTable();
            _orderDetails.Columns.Add(new DataColumn("Name"));
            _orderDetails.Columns.Add(new DataColumn("Quantity"));
            dataGridView_Orders.DataSource = _orders;
            dataGridView_OrderDetails.DataSource = _orderDetails;

            dataGridView_Orders.Columns[0].Width = 50;
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

           //Program.WCFClient.SendMessage(textBox1.Text);
        }

        public void OnPublishedEvent(Event e)
        {
            //richTextBox_Status.Text += e.EventDescription;
        }

        private bool _subscribed = false;


        public void OrderCompletionTimePushed(OrderUpdate update)
        {
            Order o = Orders.Values.First(x => x.ID == update.ID);

            foreach (DataGridViewRow r in dataGridView_Orders.Rows)
            {
                if (r.Cells[0].Value.ToString() == update.ID.ToString())
                {
                    r.Cells[1].Value = DateTime.Now.AddMinutes(
                        double.Parse(update.Minutes.ToString())).ToString();
                    break;
                }
            }
        }

        public void NewOrder(Order order, Customer customer)
        {
            Orders.Add(customer, order);

            DataRow r = _orders.NewRow();
            Order o = order;
            var c = customer;
            r[0] = o.ID;
            r[1] = o.Price;
            r[2] = c.Name;
            r[3] = c.Address;
            r[4] = c.PhoneNumber;
            r[5] = "Blank";
            _orders.Rows.Add(r);
        }

        private void UpdateTable()
        {/*
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID"));
            dt.Columns.Add(new DataColumn("Price"));
            dt.Columns.Add(new DataColumn("Customer Name"));
            dt.Columns.Add(new DataColumn("Customer Address"));
            dt.Columns.Add(new DataColumn("Customer Number"));
            dt.Columns.Add(new DataColumn("Order Status"));

            // create orders table based on entries in Orders
            foreach (Customer c in Orders.Keys)
            {
                DataRow r = dt.NewRow();
                Order o = Orders[c];
                r[0] = o.ID;
                r[1] = o.Price;
                r[2] = c.Name;
                r[3] = c.Address;
                r[4] = c.PhoneNumber;
                r[5] = o.
            }

            _orders = dt;*/
        }

        private Dictionary<Customer, Order> Orders = new Dictionary<Customer, Order>();

        private DataTable _orders;
        private DataTable _orderDetails;

        private void dataGridView_Orders_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string customerName = null;

            try
            {
                customerName = _orders.Rows[e.RowIndex].ItemArray[2].ToString();
            }
            catch
            {
                return;
            }

            // find order which matches the customer name
            Customer c = Orders.Keys.First(x => x.Name == customerName);
            Order o = Orders[c];

            _orderDetails.Rows.Clear();

            // create a row for each menu item
            foreach (string menuItem in o.Items.Keys)
            {
                DataRow r = _orderDetails.NewRow();
                r[0] = menuItem;
                r[1] = o.Items[menuItem];
                _orderDetails.Rows.Add(r);
            }
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            DataRow r = _orders.NewRow();
            r[0] = _counter++;
            r[1] = "124.24";
            r[2] = "Some name" + (_counter - 1);
            r[3] = "Some address";
            r[4] = "647-999-9999";
            r[5] = "Some status";
            Customer c = new Customer();
            c.Name = r[2].ToString();
            c.Address = r[3].ToString();
            c.PhoneNumber = r[4].ToString();
            Order o = new Order();
            o.Items = new Dictionary<string, int>();
            o.Items.Add("Some item #" + (_counter - 1).ToString(), 10);
            _orders.Rows.Add(r);

            Orders.Add(c, o);
        }

        private int _counter = 0;

        private void button_View_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("Combo #7", "10.99");
            items.Add("Duck Meat with Soy Sauce", "10.99");
            items.Add("Spring Rolls with Fries", "5.00");
            items.Add("Curry Rice with Chicken", "8.00");
            MenuView v = new MenuView(items);
            v.Activate();
            v.Show();
        }
    }
}

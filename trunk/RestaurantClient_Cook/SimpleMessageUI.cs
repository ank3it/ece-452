using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RestaurantClient_Cook.EventPublisher;
using System.Threading;

namespace RestaurantClient_Cook
{
    public partial class SimpleMessageUI : Form, IEventPublisherCallback
    {
        public SimpleMessageUI()
        {
            InitializeComponent();
            _orders = new DataTable();
            _orders.Columns.Add(new DataColumn("ID"));
            _orders.Columns.Add(new DataColumn("Completion Time"));

            _orderDetails = new DataTable();
            _orderDetails.Columns.Add(new DataColumn("Name"));
            _orderDetails.Columns.Add(new DataColumn("Quantity"));
            dataGridView_Orders.DataSource = _orders;
            dataGridView_OrderDetails.DataSource = _orderDetails;

            dataGridView_Orders.Columns[0].Width = 50;
            dataGridView_Orders.Columns[1].Width = 
                dataGridView_Orders.Columns[1].Width + 50;
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

        /// <summary>
        /// Do nothing.
        /// </summary>
        /// <param name="update"></param>
        public void OrderCompletionTimePushed(OrderUpdate update)
        {
            return;
        }

        public void NewOrder(Order order, Customer customer)
        {
            Orders.Add(order.ID.ToString(), order);

            DataRow r = _orders.NewRow();
            Order o = order;
            var c = customer;
            r[0] = o.ID;
            r[1] = "New Order";
            _orders.Rows.Add(r);
        }

        private Dictionary<string, Order> Orders = new Dictionary<string, Order>();

        public static DataTable _orders;
        private DataTable _orderDetails;

        private void dataGridView_Orders_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string id = null;

            try
            {
                id = _orders.Rows[e.RowIndex].ItemArray[0].ToString();
            }
            catch
            {
                return;
            }

            // find order which matches the customer name
            Order o = Orders[id];

            _orderDetails.Rows.Clear();

            // create a row for each menu item
            foreach (string menuItem in o.Items.Keys)
            {
                DataRow r = _orderDetails.NewRow();
                r[0] = menuItem;
                r[1] = o.Items[menuItem];
                _orderDetails.Rows.Add(r);
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                _rowIndex = e.RowIndex;
            }
        }

        private void setCookTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTimeForm f = new SetTimeForm();
            f.FormClosed += ((x, y) => UpdateCell());
            f.Activate();
            f.Show();
        }

        private void UpdateCell()
        {
            _orders.Rows[_rowIndex][1] = DateTime.Now.AddMinutes(Minutes);

            string id = _orders.Rows[_rowIndex][0].ToString();
            string time = _orders.Rows[_rowIndex][1].ToString();
            _message = string.Format("UPDATE_TIME:{0},{1}", id, Minutes.ToString());

            ThreadStart ts = new ThreadStart(Send);
            Thread t = new Thread(ts);
            t.Start();
        }

        private void Send()
        {
            try
            {
                Program.WCFClient.SendMessage(_message);
            }
            catch { }
        }

        private string _message;

        public static double Minutes;

        private int _rowIndex;
    }
}

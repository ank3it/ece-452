using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RestaurantClient_Chef
{
    public partial class MenuView : Form
    {
        public MenuView(Dictionary<string, string> menu)
        {
            InitializeComponent();
            _menuTable.Columns.Add(new DataColumn("Item"));
            _menuTable.Columns.Add(new DataColumn("Price"));
            dataGridView1.DataSource = _menuTable;
            dataGridView1.Columns[0].Width = 250;
            DisplayMenu(menu);
        }

        private void DisplayMenu(Dictionary<string, string> menu)
        {
            foreach (string item in menu.Keys)
            {
                DataRow r = _menuTable.NewRow();
                r[0] = item;
                r[1] = menu[item];
                _menu.Add(item, decimal.Parse(menu[item]));
                _menuTable.Rows.Add(r);
            }
        }

        private void button_Submit_Click(object sender, EventArgs e)
        {
            string menuItems = string.Empty;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.Cells[0].Value != null)
                {
                    menuItems += r.Cells[0].Value + "," + r.Cells[1].Value + ",";
                }
            }

            menuItems = menuItems.Substring(0, menuItems.Length - 1);

            if (!string.IsNullOrEmpty(Program.WCFClient.SendMessage(
                string.Format("{0},{1}", "Du Ma May Restaurant", menuItems))))
            {
                MessageBox.Show("Menu updated successfully!");
            }
        }

        private DataTable _menuTable = new DataTable();

        private Dictionary<string, decimal> _menu = new Dictionary<string, decimal>();

        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

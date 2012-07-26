using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RestaurantClient_Cook
{
    public partial class SetTimeForm : Form
    {
        public SetTimeForm()
        {
            InitializeComponent();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            SimpleMessageUI.Minutes = double.Parse(textBox_Minutes.Text);
            this.Close();
        }
    }
}

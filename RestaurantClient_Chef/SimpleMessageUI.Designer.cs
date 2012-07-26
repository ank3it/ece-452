namespace RestaurantClient_Chef
{
    partial class SimpleMessageUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView_Orders = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dataGridView_OrderDetails = new System.Windows.Forms.DataGridView();
            this.button_View = new System.Windows.Forms.Button();
            this.button_Add = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Orders)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_OrderDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView_Orders);
            this.groupBox3.Location = new System.Drawing.Point(12, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(635, 314);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Orders";
            // 
            // dataGridView_Orders
            // 
            this.dataGridView_Orders.AllowUserToAddRows = false;
            this.dataGridView_Orders.AllowUserToDeleteRows = false;
            this.dataGridView_Orders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Orders.Location = new System.Drawing.Point(7, 19);
            this.dataGridView_Orders.Name = "dataGridView_Orders";
            this.dataGridView_Orders.Size = new System.Drawing.Size(622, 288);
            this.dataGridView_Orders.TabIndex = 0;
            this.dataGridView_Orders.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_Orders_CellMouseClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dataGridView_OrderDetails);
            this.groupBox4.Location = new System.Drawing.Point(653, 13);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(260, 314);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Order Details";
            // 
            // dataGridView_OrderDetails
            // 
            this.dataGridView_OrderDetails.AllowUserToAddRows = false;
            this.dataGridView_OrderDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_OrderDetails.Location = new System.Drawing.Point(6, 19);
            this.dataGridView_OrderDetails.Name = "dataGridView_OrderDetails";
            this.dataGridView_OrderDetails.Size = new System.Drawing.Size(244, 288);
            this.dataGridView_OrderDetails.TabIndex = 0;
            // 
            // button_View
            // 
            this.button_View.Location = new System.Drawing.Point(838, 333);
            this.button_View.Name = "button_View";
            this.button_View.Size = new System.Drawing.Size(75, 23);
            this.button_View.TabIndex = 4;
            this.button_View.Text = "View Menu";
            this.button_View.UseVisualStyleBackColor = true;
            this.button_View.Click += new System.EventHandler(this.button_View_Click);
            // 
            // button_Add
            // 
            this.button_Add.Location = new System.Drawing.Point(739, 333);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(93, 23);
            this.button_Add.TabIndex = 5;
            this.button_Add.Text = "Add Menu Item";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Visible = false;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // SimpleMessageUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 367);
            this.Controls.Add(this.button_Add);
            this.Controls.Add(this.button_View);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Name = "SimpleMessageUI";
            this.Text = "SimpleMessageUI";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SimpleMessageUI_FormClosed);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Orders)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_OrderDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView_Orders;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dataGridView_OrderDetails;
        private System.Windows.Forms.Button button_View;
        private System.Windows.Forms.Button button_Add;
    }
}
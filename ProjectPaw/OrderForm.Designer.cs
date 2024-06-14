namespace ProjectPaw
{
    partial class OrderForm
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
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbClientId = new System.Windows.Forms.ComboBox();
            this.dtpOrder = new System.Windows.Forms.DateTimePicker();
            this.dtpDelivery = new System.Windows.Forms.DateTimePicker();
            this.clbProducts = new System.Windows.Forms.CheckedListBox();
            this.btnAddOrder = new System.Windows.Forms.Button();
            this.lvOrders = new System.Windows.Forms.ListView();
            this.colOrderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOrderDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDeliveryDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTotalAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOk = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.smSerialize = new System.Windows.Forms.ToolStripMenuItem();
            this.smDeserialize = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(81, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 20);
            this.label5.TabIndex = 29;
            this.label5.Text = "Products";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(81, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 20);
            this.label3.TabIndex = 27;
            this.label3.Text = "Delivery Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(81, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 26;
            this.label2.Text = "Order Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(81, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 25;
            this.label1.Text = "Client ID";
            // 
            // cbClientId
            // 
            this.cbClientId.FormattingEnabled = true;
            this.cbClientId.Location = new System.Drawing.Point(194, 55);
            this.cbClientId.Name = "cbClientId";
            this.cbClientId.Size = new System.Drawing.Size(238, 24);
            this.cbClientId.TabIndex = 34;
            // 
            // dtpOrder
            // 
            this.dtpOrder.Location = new System.Drawing.Point(194, 96);
            this.dtpOrder.Name = "dtpOrder";
            this.dtpOrder.Size = new System.Drawing.Size(238, 22);
            this.dtpOrder.TabIndex = 35;
            // 
            // dtpDelivery
            // 
            this.dtpDelivery.Location = new System.Drawing.Point(194, 140);
            this.dtpDelivery.Name = "dtpDelivery";
            this.dtpDelivery.Size = new System.Drawing.Size(238, 22);
            this.dtpDelivery.TabIndex = 36;
            // 
            // clbProducts
            // 
            this.clbProducts.FormattingEnabled = true;
            this.clbProducts.Location = new System.Drawing.Point(194, 188);
            this.clbProducts.Name = "clbProducts";
            this.clbProducts.Size = new System.Drawing.Size(238, 174);
            this.clbProducts.TabIndex = 37;
            // 
            // btnAddOrder
            // 
            this.btnAddOrder.BackColor = System.Drawing.Color.Thistle;
            this.btnAddOrder.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddOrder.Location = new System.Drawing.Point(533, 72);
            this.btnAddOrder.Name = "btnAddOrder";
            this.btnAddOrder.Size = new System.Drawing.Size(161, 46);
            this.btnAddOrder.TabIndex = 38;
            this.btnAddOrder.Text = "Add Order";
            this.btnAddOrder.UseVisualStyleBackColor = false;
            this.btnAddOrder.Click += new System.EventHandler(this.btnAddOrder_Click_1);
            // 
            // lvOrders
            // 
            this.lvOrders.BackColor = System.Drawing.Color.White;
            this.lvOrders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colOrderId,
            this.colOrderDate,
            this.colDeliveryDate,
            this.colStatus,
            this.colTotalAmount});
            this.lvOrders.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvOrders.FullRowSelect = true;
            this.lvOrders.HideSelection = false;
            this.lvOrders.Location = new System.Drawing.Point(-2, 384);
            this.lvOrders.Name = "lvOrders";
            this.lvOrders.Size = new System.Drawing.Size(922, 218);
            this.lvOrders.TabIndex = 39;
            this.lvOrders.UseCompatibleStateImageBehavior = false;
            this.lvOrders.View = System.Windows.Forms.View.Details;
            // 
            // colOrderId
            // 
            this.colOrderId.Text = "Order ID";
            this.colOrderId.Width = 107;
            // 
            // colOrderDate
            // 
            this.colOrderDate.Text = "Order Date";
            this.colOrderDate.Width = 193;
            // 
            // colDeliveryDate
            // 
            this.colDeliveryDate.Text = "Delivery Date";
            this.colDeliveryDate.Width = 221;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 164;
            // 
            // colTotalAmount
            // 
            this.colTotalAmount.Text = "Total Amount";
            this.colTotalAmount.Width = 233;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.Thistle;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(533, 128);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(161, 44);
            this.btnOk.TabIndex = 40;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.GhostWhite;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmFile,
            this.printToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(919, 30);
            this.menuStrip1.TabIndex = 41;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmFile
            // 
            this.tsmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smSerialize,
            this.smDeserialize});
            this.tsmFile.Name = "tsmFile";
            this.tsmFile.Size = new System.Drawing.Size(46, 26);
            this.tsmFile.Text = "File";
            this.tsmFile.Click += new System.EventHandler(this.tsmFile_Click);
            // 
            // smSerialize
            // 
            this.smSerialize.Name = "smSerialize";
            this.smSerialize.Size = new System.Drawing.Size(224, 26);
            this.smSerialize.Text = "Serialize";
            this.smSerialize.Click += new System.EventHandler(this.smSerialize_Click);
            // 
            // smDeserialize
            // 
            this.smDeserialize.Name = "smDeserialize";
            this.smDeserialize.Size = new System.Drawing.Size(224, 26);
            this.smDeserialize.Text = "Deserialize";
            this.smDeserialize.Click += new System.EventHandler(this.smDeserialize_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.printToolStripMenuItem.Text = "Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // OrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(919, 600);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lvOrders);
            this.Controls.Add(this.btnAddOrder);
            this.Controls.Add(this.clbProducts);
            this.Controls.Add(this.dtpDelivery);
            this.Controls.Add(this.dtpOrder);
            this.Controls.Add(this.cbClientId);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "OrderForm";
            this.Text = "Order";
            this.Load += new System.EventHandler(this.OrderForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbClientId;
        private System.Windows.Forms.DateTimePicker dtpOrder;
        private System.Windows.Forms.DateTimePicker dtpDelivery;
        private System.Windows.Forms.CheckedListBox clbProducts;
        private System.Windows.Forms.Button btnAddOrder;
        private System.Windows.Forms.ListView lvOrders;
        private System.Windows.Forms.ColumnHeader colOrderId;
        private System.Windows.Forms.ColumnHeader colOrderDate;
        private System.Windows.Forms.ColumnHeader colDeliveryDate;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colTotalAmount;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmFile;
        private System.Windows.Forms.ToolStripMenuItem smSerialize;
        private System.Windows.Forms.ToolStripMenuItem smDeserialize;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
    }
}
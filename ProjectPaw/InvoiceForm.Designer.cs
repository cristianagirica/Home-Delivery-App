namespace ProjectPaw
{
    partial class InvoiceForm
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
            this.lvInvoices = new System.Windows.Forms.ListView();
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOrderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colClientId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvInvoices
            // 
            this.lvInvoices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colId,
            this.colOrderId,
            this.colName,
            this.colClientId,
            this.colDate,
            this.colAmount});
            this.lvInvoices.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvInvoices.HideSelection = false;
            this.lvInvoices.Location = new System.Drawing.Point(12, 65);
            this.lvInvoices.Name = "lvInvoices";
            this.lvInvoices.Size = new System.Drawing.Size(895, 523);
            this.lvInvoices.TabIndex = 0;
            this.lvInvoices.UseCompatibleStateImageBehavior = false;
            this.lvInvoices.View = System.Windows.Forms.View.Details;
            // 
            // colId
            // 
            this.colId.Text = "Invoice Id";
            this.colId.Width = 180;
            // 
            // colOrderId
            // 
            this.colOrderId.Text = "Order Id";
            this.colOrderId.Width = 168;
            // 
            // colName
            // 
            this.colName.Text = "Client Name";
            this.colName.Width = 221;
            // 
            // colClientId
            // 
            this.colClientId.Text = "Client Id";
            this.colClientId.Width = 110;
            // 
            // colDate
            // 
            this.colDate.Text = "Date";
            this.colDate.Width = 108;
            // 
            // colAmount
            // 
            this.colAmount.Text = "Total Amount";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Thistle;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(13, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 33);
            this.button1.TabIndex = 1;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // InvoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 600);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lvInvoices);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InvoiceForm";
            this.Text = "InvoiceForm";
            this.Load += new System.EventHandler(this.InvoiceForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvInvoices;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colOrderId;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colClientId;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colAmount;
        private System.Windows.Forms.Button button1;
    }
}
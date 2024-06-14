namespace ProjectPaw
{
    partial class OrderDetailsForm
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
            this.lvOr = new System.Windows.Forms.ListView();
            this.colOrderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colClientId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOrderDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDeliveryDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colItems = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvOr
            // 
            this.lvOr.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colOrderId,
            this.colClientId,
            this.colOrderDate,
            this.colDeliveryDate,
            this.colItems,
            this.colAmount,
            this.colStatus});
            this.lvOr.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvOr.HideSelection = false;
            this.lvOr.Location = new System.Drawing.Point(44, 149);
            this.lvOr.Name = "lvOr";
            this.lvOr.Size = new System.Drawing.Size(830, 139);
            this.lvOr.TabIndex = 1;
            this.lvOr.UseCompatibleStateImageBehavior = false;
            this.lvOr.View = System.Windows.Forms.View.Details;
            // 
            // colOrderId
            // 
            this.colOrderId.Text = "Order Id";
            this.colOrderId.Width = 75;
            // 
            // colClientId
            // 
            this.colClientId.Text = "Client Id";
            this.colClientId.Width = 74;
            // 
            // colOrderDate
            // 
            this.colOrderDate.Text = "Order Date";
            this.colOrderDate.Width = 121;
            // 
            // colDeliveryDate
            // 
            this.colDeliveryDate.Text = "Delivery Date";
            this.colDeliveryDate.Width = 131;
            // 
            // colItems
            // 
            this.colItems.Text = "Products";
            this.colItems.Width = 228;
            // 
            // colAmount
            // 
            this.colAmount.Text = "Amount";
            this.colAmount.Width = 101;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 99;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(512, 345);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(151, 42);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // OrderDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 600);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lvOr);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OrderDetailsForm";
            this.Text = "OrderDetailsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvOr;
        private System.Windows.Forms.ColumnHeader colOrderId;
        private System.Windows.Forms.ColumnHeader colClientId;
        private System.Windows.Forms.ColumnHeader colOrderDate;
        private System.Windows.Forms.ColumnHeader colDeliveryDate;
        private System.Windows.Forms.ColumnHeader colItems;
        private System.Windows.Forms.ColumnHeader colAmount;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.Button btnCancel;
    }
}
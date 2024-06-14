using Project.Entitites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectPaw
{
    public partial class InvoiceForm : Form
    {
        private string ConnectionString = "Data Source=database.db";
        public static List<Invoice> Invoices = new List<Invoice>();
        public InvoiceForm()
        {
            InitializeComponent();
            PopulateInvoices();
        }
        private void LoadInvoices()
        {
            Invoices.Clear(); 

            string query = "SELECT * FROM Invoice";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(query, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int invoiceId = reader.GetInt32(reader.GetOrdinal("InvoiceId"));
                        int orderId = reader.GetInt32(reader.GetOrdinal("OrderId"));
                        int clientId = reader.GetInt32(reader.GetOrdinal("ClientId"));
                        DateTime invoiceDate = reader.GetDateTime(reader.GetOrdinal("InvoiceDate"));
                        double amount = reader.GetDouble(reader.GetOrdinal("Amount"));

                        Invoice invoice = new Invoice
                        {
                            InvoiceID = invoiceId,
                            OrderID = orderId,
                            ClientID = clientId,
                            InvoiceDate = invoiceDate,
                            Amount = amount
                        };

                        Invoices.Add(invoice);
                    }
                }
            }
        }
        private void PopulateInvoices()
        {
            lvInvoices.Items.Clear(); 

            foreach (var invoice in Invoices)
            {
                var client = ClientForm.Clients.FirstOrDefault(c => c.ClientID == invoice.ClientID);
                string clientName = client != null ? client.Name : "Unknown Client";

                ListViewItem item = new ListViewItem(invoice.InvoiceID.ToString());
                item.SubItems.Add(invoice.OrderID.ToString());
                item.SubItems.Add(clientName);
                item.SubItems.Add(invoice.ClientID.ToString());
                item.SubItems.Add(invoice.InvoiceDate.ToString("yyyy-MM-dd"));
                item.SubItems.Add(invoice.Amount.ToString("C"));
                lvInvoices.Items.Add(item);
            }
        }
        private void InvoiceForm_Load(object sender, EventArgs e)
        {
            LoadInvoices();
            PopulateInvoices();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

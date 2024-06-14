using Project.Entitites;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing.Printing;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static Project.Entitites.Order;
using static System.Windows.Forms.ListViewItem;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ProjectPaw
{
    public partial class OrderForm : Form
    {
        private string ConnectionString = "Data Source=database.db";
        public static List<Order> Orders = new List<Order>();
        public static List<Invoice> Invoices = new List<Invoice>();

        private PrintDocument printDocument = new PrintDocument();
        private PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();

        public OrderForm()
        {
            InitializeComponent();
            PopulateProducts();
            PopulateClientIDs();
            DisplayClients();
            lvOrders.ItemActivate += lvOrders_ItemActivate; 

            InitializeOrderListView();
        }

        private void PopulateProducts()
        {
            List<string> products = new List<string>
            {
                "Red Roses Bouquet",
                "Pink Peonies Bouquet",
                "Lavender Bouquet",
                "Sunflower Bouquet",
                "Tulip Bouquet",
                "Orchid Arrangement",
                "Daisy Bouquet",
                "Mixed Flowers Bouquet",
                "White Lilies Bouquet",
                "Blue Hydrangeas Bouquet",
                "Carnation Bouquet",
                "Calla Lily Bouquet",
                "Gerbera Daisy Bouquet",
                "Potted Orchid",
                "Peony Arrangement",
                "Rose and Lily Bouquet",
                "Wildflower Bouquet",
                "Cactus Plant",
                "Succulent Garden",
                "Bonsai Tree",
            };

            foreach (string product in products)
            {
                clbProducts.Items.Add(product);
            }
        }

        private void LoadClients()
        {
            using (ClientForm clientForm = new ClientForm())
            {
                clientForm.LoadClients(); 
            }
        }

        private void PopulateClientIDs()
        {
            cbClientId.Items.Clear();
            if (ClientForm.Clients != null && ClientForm.Clients.Count > 0)
            {
                foreach (var client in ClientForm.Clients)
                {
                    cbClientId.Items.Add(client.ClientID);
                }
            }
            else
            {
                MessageBox.Show("Client list is empty or null.");
            }
        }

        private void DisplayClients()
        {
            cbClientId.Items.Clear();
            if (ClientForm.Clients != null)
            {
                foreach (var client in ClientForm.Clients)
                {
                    cbClientId.Items.Add(client.ClientID);
                }
            }
        }

        private void AddOrderDB(Order order)
        {
            string query = "INSERT INTO Orders(ClientId, OrderDate, DeliveryDate, Items, Amount, Status) " +
                           "VALUES (@ClientId, @OrderDate, @DeliveryDate, @Items, @Amount, @Status)";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClientId", order.ClientID);
                    command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    command.Parameters.AddWithValue("@DeliveryDate", order.DeliveryDate);
                    command.Parameters.AddWithValue("@Items", string.Join(", ", order.Items));
                    command.Parameters.AddWithValue("@Amount", order.Amount);
                    command.Parameters.AddWithValue("@Status", order.Status.ToString());

                    command.ExecuteNonQuery();
                    order.OrderID = (int)connection.LastInsertRowId;
                }

                Orders.Add(order);
                Invoice newInvoice = new Invoice
                {
                    OrderID = order.OrderID,
                    ClientID = order.ClientID,
                    InvoiceDate = order.OrderDate,
                    Amount = order.Amount
                };
                AddInvoiceDB(newInvoice);
            }

          
            Client client = ClientForm.Clients.FirstOrDefault(c => c.ClientID == order.ClientID);
            if (client != null)
            {
                client.Orders.Add(order);
                UpdateClientOrders(client);
            }
        }

        private void AddInvoiceDB(Invoice invoice)
        {
            string query = "INSERT INTO Invoice(OrderId, ClientId, InvoiceDate, Amount) " +
                           "VALUES (@OrderId, @ClientId, @InvoiceDate, @Amount)";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", invoice.OrderID);
                    command.Parameters.AddWithValue("@ClientId", invoice.ClientID);
                    command.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
                    command.Parameters.AddWithValue("@Amount", invoice.Amount);

                    command.ExecuteNonQuery();
                    invoice.InvoiceID = (int)connection.LastInsertRowId;
                }

                Invoices.Add(invoice);
            }
        }

        private void UpdateClientOrders(Client client)
        {
            if (client != null)
            {
                string updatedOrders = string.Join(",", client.Orders.Select(o => o.OrderID));
                string query = "UPDATE Client SET Orders = @Orders WHERE ClientId = @ClientId";

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Orders", updatedOrders);
                        command.Parameters.AddWithValue("@ClientId", client.ClientID);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private void LoadOrders()
        {
            string query = "SELECT * FROM Orders";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(query, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("OrderId"));
                        int clientId = reader.GetInt32(reader.GetOrdinal("ClientId"));
                        DateTime orderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate"));
                        DateTime deliveryDate = reader.GetDateTime(reader.GetOrdinal("DeliveryDate"));
                        string items = reader.GetString(reader.GetOrdinal("Items"));

                        List<string> Items = new List<string>();
                        if (!string.IsNullOrWhiteSpace(items))
                        {
                            Items = items.Split(',').Select(item => item.Trim()).ToList();
                        }

                        double amount = reader.GetDouble(reader.GetOrdinal("Amount"));
                        string status = reader.GetString(reader.GetOrdinal("Status"));
                        DeliveryStatus deliveryStatus = (DeliveryStatus)Enum.Parse(typeof(DeliveryStatus), status);

                        Order order = new Order(id, clientId, orderDate, deliveryDate, Items, amount, deliveryStatus);

                        Orders.Add(order);
                    }
                }
            }
        }

        private void InitializeOrderListView()
        {
            lvOrders.View = View.Details;
            lvOrders.FullRowSelect = true;

            lvOrders.Columns.Add("Order ID", 100);
            lvOrders.Columns.Add("Order Date", 100);
            lvOrders.Columns.Add("Delivery Date", 100);
            lvOrders.Columns.Add("Status", 100);
            lvOrders.Columns.Add("Amount", 100);
        }

        private void UpdateOrderListView(Order order)
        {
            ListViewItem item = new ListViewItem(order.OrderID.ToString());
            item.SubItems.Add(order.OrderDate.ToString("yyyy-MM-dd"));
            item.SubItems.Add(order.DeliveryDate.ToString("yyyy-MM-dd"));
            item.SubItems.Add(order.Status.ToString());
            item.SubItems.Add(order.Amount.ToString("C"));

            lvOrders.Items.Add(item);
        }

        public static int SelectedOrderId { get; private set; }

        private void lvOrders_ItemActivate(object sender, EventArgs e)
        {
            if (lvOrders.SelectedItems.Count > 0)
            {
                int selectedOrderId = int.Parse(lvOrders.SelectedItems[0].SubItems[0].Text);

                Order selectedOrder = Orders.FirstOrDefault(c => c.OrderID == selectedOrderId);

                if (selectedOrder != null)
                {
                    OpenOrderDetailsForm(selectedOrder);
                }
            }
        }

        private void OpenOrderDetailsForm(Order order)
        {
            OrderDetailsForm detailsForm = new OrderDetailsForm();
            detailsForm.Owner = this;
            detailsForm.TopLevel = false;
            detailsForm.FormBorderStyle = FormBorderStyle.None;
            detailsForm.Dock = DockStyle.Fill;
            this.Controls.Add(detailsForm);
            detailsForm.BringToFront();
            detailsForm.Show();

            detailsForm.LoadOrderDetails(order.OrderID);
        }

        private void PopulateOrderListView()
        {
            lvOrders.Items.Clear();
            foreach (var order in Orders)
            {
                ListViewItem item = new ListViewItem(order.OrderID.ToString());
                item.SubItems.Add(order.OrderDate.ToString("yyyy-MM-dd"));
                item.SubItems.Add(order.DeliveryDate.ToString("yyyy-MM-dd"));
                item.SubItems.Add(order.Status.ToString());
                item.SubItems.Add(order.Amount.ToString("C"));
                lvOrders.Items.Add(item);
            }
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            LoadOrders();
            PopulateOrderListView();
            PopulateClientIDs(); 
        }

        private void btnAddOrder_Click_1(object sender, EventArgs e)
        {
            // Validare pentru cbClientId: trebuie să fie selectat din combo box
            if (cbClientId.SelectedItem == null)
            {
                MessageBox.Show("Please select a client.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validare pentru dtpDelivery: data de livrare trebuie să fie după data comenzii
            if (dtpDelivery.Value <= dtpOrder.Value)
            {
                MessageBox.Show("Delivery date must be after the order date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validare pentru clbProducts: trebuie să fie selectat cel puțin un produs
            if (clbProducts.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one product.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Order newOrder = new Order
            {
                ClientID = Convert.ToInt32(cbClientId.SelectedItem),
                OrderDate = dtpOrder.Value,
                DeliveryDate = dtpDelivery.Value,
                Items = new List<string>()
            };

            foreach (var item in clbProducts.CheckedItems)
            {
                newOrder.Items.Add(item.ToString());
            }

            newOrder.Amount = newOrder.CalculateAmount();
            newOrder.Status = DeliveryStatus.Pending;

            if (newOrder.DeliveryDate < DateTime.Now)
            {
                newOrder.Status = DeliveryStatus.Delivered;
            }

            AddOrderDB(newOrder);

            Invoice newInvoice = new Invoice
            {
                OrderID = newOrder.OrderID,
                ClientID = newOrder.ClientID,
                InvoiceDate = newOrder.OrderDate,
                Amount = newOrder.Amount
            };
            Invoices.Add(newInvoice);

            MessageBox.Show($"Order Added:\nOrder ID: {newOrder.OrderID}\nClient ID: {newOrder.ClientID}\nOrder Date: {newOrder.OrderDate}\nDelivery Date: {newOrder.DeliveryDate}\nAmount: {newOrder.Amount}\nStatus: {newOrder.Status}");

            for (int i = 0; i < clbProducts.Items.Count; i++)
            {
                clbProducts.SetItemChecked(i, false);
            }

           
            UpdateOrderListView(newOrder);

          
            DisplayClients();
        }

        private void tsmFile_Click(object sender, EventArgs e)
        {

        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.ShowDialog();
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
           
            Font font = new Font("Arial", 12);
            float fontHeight = font.GetHeight();
            int startX = 10;
            int startY = 10;
            int offsetY = 0;

          
            e.Graphics.DrawString("Order Report", new Font("Arial", 18, FontStyle.Bold), Brushes.Black, startX, startY + offsetY);
            offsetY += (int)fontHeight + 20;

           
            foreach (ColumnHeader column in lvOrders.Columns)
            {
                e.Graphics.DrawString(column.Text, font, Brushes.Black, startX, startY + offsetY);
                startX += column.Width;
            }
            offsetY += (int)fontHeight + 5;
            startX = 10;

            foreach (ListViewItem item in lvOrders.Items)
            {
                startX = 10;
                foreach (ListViewSubItem subItem in item.SubItems)
                {
                    e.Graphics.DrawString(subItem.Text, font, Brushes.Black, startX, startY + offsetY);
                    startX += lvOrders.Columns[item.SubItems.IndexOf(subItem)].Width;
                }
                offsetY += (int)fontHeight + 5;
            }
        }

        private void smSerialize_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                using (FileStream stream = File.Create("orders.bin"))
                {
                    formatter.Serialize(stream, Orders);
                }
                MessageBox.Show("Orders serialized successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while serializing the orders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void smDeserialize_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                using (FileStream stream = File.OpenRead("orders.bin"))
                {
                    Orders = (List<Order>)formatter.Deserialize(stream);
                    PopulateOrderListView(); 
                }
                MessageBox.Show("Orders deserialized successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deserializing the orders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

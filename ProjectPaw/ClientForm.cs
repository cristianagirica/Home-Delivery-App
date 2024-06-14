//using Project.Entitites;
//using System;
//using System.Collections.Generic;
//using System.Data.SQLite;
//using System.Linq;
//using System.Windows.Forms;

//namespace ProjectPaw
//{
//    public partial class ClientForm : Form
//    {
//        public ListView LvClients;
//        public Client Client { get; set; }
//        public static List<Client> Clients { get; set; }
//        private string ConnectionString = "Data Source=Database.db";

//        public ClientForm()
//        {
//            InitializeComponent();
//            if (Clients == null)
//            {
//                Clients = new List<Client>();
//            }
//            CreateClientsTable(); // Asigură-te că tabela este creată
//            PopulateCountiesComboBox();
//            SetupPhoneNumberInput();
//            InitializeListView();

//            this.Activated += new EventHandler(ClientForm_Activated);
//            lvClients.ItemActivate += lvClients_ItemActivate;
//        }

//        private void CreateClientsTable()
//        {
//            string query = @"
//            CREATE TABLE IF NOT EXISTS Client (
//                ClientId INTEGER PRIMARY KEY AUTOINCREMENT,
//                Name TEXT NOT NULL,
//                Address TEXT NOT NULL,
//                Phone TEXT NOT NULL,
//                Email TEXT NOT NULL,
//                Orders TEXT
//            );";

//            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
//            {
//                connection.Open();
//                SQLiteCommand command = new SQLiteCommand(query, connection);
//                command.ExecuteNonQuery();
//            }
//        }

//        private void InitializeListView()
//        {
//            lvClients.View = View.Details;
//            lvClients.FullRowSelect = true;

//            lvClients.Columns.Add("ID", 50);
//            lvClients.Columns.Add("Name", 150);
//        }

//        private void SetupPhoneNumberInput()
//        {
//            mtbPhone.Mask = "0000 000 000";
//        }

//        private void PopulateCountiesComboBox()
//        {
//            List<string> counties = new List<string>
//            {
//                "Alba", "Arad", "Argeș", "Bacău", "Bihor", "Bistrița-Năsăud", "Botoșani", "Brașov", "Brăila", "Buzău",
//                "Caraș-Severin", "Călărași", "Cluj", "Constanța", "Covasna", "Dâmbovița", "Dolj", "Galați", "Giurgiu",
//                "Gorj", "Harghita", "Hunedoara", "Ialomița", "Iași", "Ilfov", "Maramureș", "Mehedinți", "Mureș",
//                "Neamț", "Olt", "Prahova", "Satu Mare", "Sălaj", "Sibiu", "Suceava", "Teleorman", "Timiș",
//                "Tulcea", "Vaslui", "Vâlcea", "Vrancea", "București"
//            };

//            counties.Sort();
//            cbCounty.DataSource = counties;
//        }

//        private void ClientForm_Load(object sender, EventArgs e)
//        {
//            LoadClients();
//            DisplayClients();

//            if (Client != null)
//            {
//                tbName.Text = Client.Name;
//                mtbPhone.Text = Client.Phone;
//                tbEmail.Text = Client.Email;

//                if (Client.Address != null)
//                {
//                    string[] addressParts = Client.Address.Split(',');
//                    if (addressParts.Length >= 4)
//                    {
//                        cbCountry.Text = addressParts[0].Trim();
//                        cbCounty.Text = addressParts[1].Trim();
//                        tbCity.Text = addressParts[2].Trim();
//                        tbAddress.Text = string.Join(", ", addressParts.Skip(3)).Trim();
//                    }
//                }
//            }
//        }

//        private void AddClientDB(Client client)
//        {
//            string query = "INSERT INTO Client (Name, Address, Phone, Email, Orders) VALUES (@Name, @Address, @Phone, @Email, @Orders)";

//            try
//            {
//                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
//                {
//                    connection.Open();
//                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
//                    {
//                        command.Parameters.AddWithValue("@Name", client.Name);
//                        command.Parameters.AddWithValue("@Address", client.Address);
//                        command.Parameters.AddWithValue("@Phone", client.Phone);
//                        command.Parameters.AddWithValue("@Email", client.Email);
//                        command.Parameters.AddWithValue("@Orders", string.Join(",", client.Orders.Select(o => o.OrderID)));

//                        command.ExecuteNonQuery();
//                        client.ClientID = (int)connection.LastInsertRowId; // Obține ID-ul generat automat
//                        Clients.Add(client);
//                    }
//                }
//            }
//            catch (SQLiteException ex)
//            {
//                MessageBox.Show($"SQLite error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void DeleteClientDB(Client client)
//        {
//            string query = "DELETE FROM Client WHERE ClientId = @id";

//            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
//            {
//                connection.Open();

//                SQLiteCommand command = new SQLiteCommand(query, connection);
//                command.Parameters.AddWithValue("@id", client.ClientID);
//                command.ExecuteNonQuery();

//                Clients.Remove(client);
//            }
//        }
//        public void LoadClients()
//        {
//            string query = "SELECT * FROM Client";

//            try
//            {
//                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
//                {
//                    connection.Open();
//                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
//                    {
//                        using (SQLiteDataReader reader = command.ExecuteReader())
//                        {
//                            Clients.Clear(); // Asigură-te că lista este goală înainte de a adăuga clienți din baza de date

//                            while (reader.Read())
//                            {
//                                int id = reader.GetInt32(reader.GetOrdinal("ClientId"));
//                                string name = reader.GetString(reader.GetOrdinal("Name"));
//                                string address = reader.GetString(reader.GetOrdinal("Address"));
//                                string phone = reader.GetString(reader.GetOrdinal("Phone"));
//                                string email = reader.GetString(reader.GetOrdinal("Email"));
//                                string orders = reader.IsDBNull(reader.GetOrdinal("Orders")) ? "" : reader.GetString(reader.GetOrdinal("Orders"));

//                                List<Order> Orders = new List<Order>();
//                                if (!string.IsNullOrWhiteSpace(orders))
//                                {
//                                    foreach (var orderId in orders.Split(','))
//                                    {
//                                        if (int.TryParse(orderId, out int idOrder))
//                                        {
//                                            var order = MainForm.Orders.FirstOrDefault(o => o.OrderID == idOrder);
//                                            if (order != null)
//                                            {
//                                                Orders.Add(order);
//                                            }
//                                        }
//                                    }
//                                }

//                                Client client = new Client(id, name, address, phone, email, Orders);
//                                Clients.Add(client);
//                            }
//                            // Actualizează lista de clienți globală
//                            MainForm.Clients = Clients;
//                        }
//                    }
//                }
//            }
//            catch (SQLiteException ex)
//            {
//                MessageBox.Show($"SQLite error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }


//        public void DisplayClients()
//        {
//            lvClients.Items.Clear();
//            foreach (var client in Clients)
//            {
//                ListViewItem lvi = new ListViewItem(client.ClientID.ToString());
//                lvi.SubItems.Add(client.Name);
//                lvi.SubItems.Add(client.Address);
//                lvi.SubItems.Add(client.Phone);
//                lvi.SubItems.Add(client.Email);
//                lvi.SubItems.Add(string.Join(", ", client.Orders.Select(o => o.OrderID))); // Afișează comenzile

//                lvi.Tag = client;
//                lvClients.Items.Add(lvi);
//            }
//        }


//        private void ClientForm_Activated(object sender, EventArgs e)
//        {
//            DisplayClients();
//        }

//        private void button1_Click(object sender, EventArgs e)
//        {
//            if (string.IsNullOrWhiteSpace(tbName.Text))
//            {
//                MessageBox.Show("Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            if (string.IsNullOrWhiteSpace(mtbPhone.Text.Replace(" ", "")))
//            {
//                MessageBox.Show("Phone number cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            if (string.IsNullOrWhiteSpace(tbEmail.Text))
//            {
//                MessageBox.Show("Email cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            if (string.IsNullOrWhiteSpace(cbCountry.Text) || string.IsNullOrWhiteSpace(cbCounty.Text) || string.IsNullOrWhiteSpace(tbCity.Text) || string.IsNullOrWhiteSpace(tbAddress.Text))
//            {
//                MessageBox.Show("Address fields cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            Client client = new Client
//            {
//                Name = tbName.Text,
//                Phone = mtbPhone.Text,
//                Email = tbEmail.Text,
//                Address = $"{cbCountry.Text.Trim()}, {cbCounty.Text.Trim()}, {tbCity.Text.Trim()}, {tbAddress.Text.Trim()}",
//                Orders = new List<Order>()
//            };

//            AddClientDB(client);
//            DisplayClients();
//        }

//        private Order GetOrderById(int orderID)
//        {
//            foreach (var ord in MainForm.Orders)
//            {
//                if (ord.OrderID == orderID)
//                {
//                    return ord;
//                }
//            }
//            return null;
//        }

//        protected override void OnLoad(EventArgs e)
//        {
//            base.OnLoad(e);
//            DisplayClients();
//        }

//        public static int SelectedClientId { get; private set; }

//        private void lvClients_ItemActivate(object sender, EventArgs e)
//        {
//            if (lvClients.SelectedItems.Count > 0)
//            {
//                int selectedClientId = int.Parse(lvClients.SelectedItems[0].SubItems[0].Text);

//                Client selectedClient = Clients.FirstOrDefault(c => c.ClientID == selectedClientId);

//                if (selectedClient != null)
//                {
//                    OpenClientDetailsForm(selectedClient);
//                }
//            }
//        }

//        private void OpenClientDetailsForm(Client client)
//        {
//            ClientDetailsForm detailsForm = new ClientDetailsForm();
//            detailsForm.Owner = this;
//            detailsForm.TopLevel = false;
//            detailsForm.FormBorderStyle = FormBorderStyle.None;
//            detailsForm.Dock = DockStyle.Fill;
//            this.Controls.Add(detailsForm);
//            detailsForm.BringToFront();
//            detailsForm.Show();

//            detailsForm.LoadClientDetails(client.ClientID);
//        }

//        private void btnDelete_Click(object sender, EventArgs e)
//        {
//            if(lvClients.SelectedItems.Count == 0) 
//            {
//                MessageBox.Show("Choose participant!");
//                return;
//            }

//            if(MessageBox.Show("Are you sure you want to delete this client?", "Delete Client", 
//                MessageBoxButtons.OKCancel,
//                MessageBoxIcon.Warning) == DialogResult.OK)
//            {
//                ListViewItem selectedItem = lvClients.SelectedItems[0];
//                Client client = (Client)selectedItem.Tag;

//                DeleteClientDB(client);
//                DisplayClients();
//            }
//        }
//    }
//}

using Project.Entitites;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProjectPaw
{
    public partial class ClientForm : Form
    {
        public Client Client { get; set; }
        public static List<Client> Clients { get; set; }
        private string ConnectionString = "Data Source=Database.db";

        public ClientForm()
        {
            InitializeComponent();
            if (Clients == null)
            {
                Clients = new List<Client>();
            }
            CreateClientsTable(); 
            PopulateCountiesComboBox();
            SetupPhoneNumberInput();

            this.Activated += new EventHandler(ClientForm_Activated);
            lvClients.ItemActivate += lvClients_ItemActivate;
        }

        private void CreateClientsTable()
        {
            string query = @"
            CREATE TABLE IF NOT EXISTS Client (
                ClientId INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Address TEXT NOT NULL,
                Phone TEXT NOT NULL,
                Email TEXT NOT NULL,
                Orders TEXT
            );";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }

      
        private void SetupPhoneNumberInput()
        {
            mtbPhone.Mask = "0000 000 000";
        }

        private void PopulateCountiesComboBox()
        {
            List<string> counties = new List<string>
            {
                "Alba", "Arad", "Argeș", "Bacău", "Bihor", "Bistrița-Năsăud", "Botoșani", "Brașov", "Brăila", "Buzău",
                "Caraș-Severin", "Călărași", "Cluj", "Constanța", "Covasna", "Dâmbovița", "Dolj", "Galați", "Giurgiu",
                "Gorj", "Harghita", "Hunedoara", "Ialomița", "Iași", "Ilfov", "Maramureș", "Mehedinți", "Mureș",
                "Neamț", "Olt", "Prahova", "Satu Mare", "Sălaj", "Sibiu", "Suceava", "Teleorman", "Timiș",
                "Tulcea", "Vaslui", "Vâlcea", "Vrancea", "București"
            };

            counties.Sort();
            cbCounty.DataSource = counties;
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            LoadClients();
            DisplayClients();

            if (Client != null)
            {
                tbName.Text = Client.Name;
                mtbPhone.Text = Client.Phone;
                tbEmail.Text = Client.Email;

                if (Client.Address != null)
                {
                    string[] addressParts = Client.Address.Split(',');
                    if (addressParts.Length >= 4)
                    {
                        cbCountry.Text = addressParts[0].Trim();
                        cbCounty.Text = addressParts[1].Trim();
                        tbCity.Text = addressParts[2].Trim();
                        tbAddress.Text = string.Join(", ", addressParts.Skip(3)).Trim();
                    }
                }
            }
        }

        private void AddClientDB(Client client)
        {
            string query = "INSERT INTO Client (Name, Address, Phone, Email, Orders) VALUES (@Name, @Address, @Phone, @Email, @Orders)";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", client.Name);
                        command.Parameters.AddWithValue("@Address", client.Address);
                        command.Parameters.AddWithValue("@Phone", client.Phone);
                        command.Parameters.AddWithValue("@Email", client.Email);
                        command.Parameters.AddWithValue("@Orders", string.Join(",", client.Orders.Select(o => o.OrderID)));

                        command.ExecuteNonQuery();
                        client.ClientID = (int)connection.LastInsertRowId; 
                        Clients.Add(client);
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"SQLite error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteClientDB(Client client)
        {

           
            DeleteClientOrdersAndInvoices(client.ClientID);

           
            string query = "DELETE FROM Client WHERE ClientId = @id";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", client.ClientID);
                    command.ExecuteNonQuery();

                    Clients.Remove(client);
                }
            }
        }

        private void DeleteClientOrdersAndInvoices(int clientId)
        {
          
            string deleteOrdersQuery = "DELETE FROM Orders WHERE ClientId = @ClientId";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(deleteOrdersQuery, connection))
                {
                    command.Parameters.AddWithValue("@ClientId", clientId);
                    command.ExecuteNonQuery();
                }
            }

            
            var clientOrders = OrderForm.Orders.Where(o => o.ClientID == clientId).ToList();
            foreach (var order in clientOrders)
            {
                OrderForm.Orders.Remove(order);
            }

            // Șterge facturile clientului
            string deleteInvoicesQuery = "DELETE FROM Invoice WHERE ClientId = @ClientId";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(deleteInvoicesQuery, connection))
                {
                    command.Parameters.AddWithValue("@ClientId", clientId);
                    command.ExecuteNonQuery();
                }
            }

            // Șterge facturile din lista locală Invoices
            var clientInvoices = OrderForm.Invoices.Where(i => i.ClientID == clientId).ToList();
            foreach (var invoice in clientInvoices)
            {
                OrderForm.Invoices.Remove(invoice);
            }
        }


        public void LoadClients()
        {
            string query = "SELECT * FROM Client";

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            Clients.Clear();

                            while (reader.Read())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("ClientId"));
                                string name = reader.GetString(reader.GetOrdinal("Name"));
                                string address = reader.GetString(reader.GetOrdinal("Address"));
                                string phone = reader.GetString(reader.GetOrdinal("Phone"));
                                string email = reader.GetString(reader.GetOrdinal("Email"));
                                string orders = reader.IsDBNull(reader.GetOrdinal("Orders")) ? "" : reader.GetString(reader.GetOrdinal("Orders"));

                                List<Order> Orders = new List<Order>();
                                if (!string.IsNullOrWhiteSpace(orders))
                                {
                                    foreach (var orderId in orders.Split(','))
                                    {
                                        if (int.TryParse(orderId, out int idOrder))
                                        {
                                            var order = OrderForm.Orders.FirstOrDefault(o => o.OrderID == idOrder);
                                            if (order != null)
                                            {
                                                Orders.Add(order);
                                            }
                                        }
                                    }
                                }

                                Client client = new Client(id, name, address, phone, email, Orders);
                                Clients.Add(client);
                            }
                           
                            MainForm.Clients = Clients;
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"SQLite error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DisplayClients()
        {
            lvClients.Items.Clear();
            foreach (var client in Clients)
            {
                ListViewItem lvi = new ListViewItem(client.ClientID.ToString());
                lvi.SubItems.Add(client.Name);
                lvi.SubItems.Add(client.Address);
                lvi.SubItems.Add(client.Phone);
                lvi.SubItems.Add(client.Email);
                lvi.SubItems.Add(string.Join(", ", client.Orders.Select(o => o.OrderID))); 

                lvi.Tag = client;
                lvClients.Items.Add(lvi);
            }
        }

        private void ClientForm_Activated(object sender, EventArgs e)
        {
            DisplayClients();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validare pentru tbName: trebuie să fie format din cel puțin două cuvinte
            if (string.IsNullOrWhiteSpace(tbName.Text) || tbName.Text.Split(' ').Length < 2)
            {
                MessageBox.Show("Name must be at least two words.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validare pentru mtbPhone: trebuie să fie unic în baza de date
            if (string.IsNullOrWhiteSpace(mtbPhone.Text.Replace(" ", "")))
            {
                MessageBox.Show("Phone number cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string checkPhoneQuery = "SELECT COUNT(*) FROM Client WHERE Phone = @Phone";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(checkPhoneQuery, connection))
                {
                    command.Parameters.AddWithValue("@Phone", mtbPhone.Text);
                    int phoneCount = Convert.ToInt32(command.ExecuteScalar());

                    if (phoneCount > 0)
                    {
                        MessageBox.Show("Phone number must be unique.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            // Validare pentru tbEmail: trebuie să aibă un format valid
            if (string.IsNullOrWhiteSpace(tbEmail.Text) || !Regex.IsMatch(tbEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validare pentru cbCountry: trebuie să fie selectat
            if (string.IsNullOrWhiteSpace(cbCountry.Text))
            {
                MessageBox.Show("Country must be selected.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validare pentru cbCounty: trebuie să fie selectat din listă
            if (string.IsNullOrWhiteSpace(cbCounty.Text) || !cbCounty.Items.Contains(cbCounty.Text))
            {
                MessageBox.Show("County must be selected from the list.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validare pentru restul câmpurilor de adresă
            if (string.IsNullOrWhiteSpace(tbCity.Text) || string.IsNullOrWhiteSpace(tbAddress.Text))
            {
                MessageBox.Show("Address fields cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Client client = new Client
            {
                Name = tbName.Text,
                Phone = mtbPhone.Text,
                Email = tbEmail.Text,
                Address = $"{cbCountry.Text.Trim()}, {cbCounty.Text.Trim()}, {tbCity.Text.Trim()}, {tbAddress.Text.Trim()}",
                Orders = new List<Order>()
            };

            AddClientDB(client);
            DisplayClients();
        }

        private Order GetOrderById(int orderID)
        {
            foreach (var ord in MainForm.Orders)
            {
                if (ord.OrderID == orderID)
                {
                    return ord;
                }
            }
            return null;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DisplayClients();
        }

        public static int SelectedClientId { get; private set; }

        private void lvClients_ItemActivate(object sender, EventArgs e)
        {
            if (lvClients.SelectedItems.Count > 0)
            {
                int selectedClientId = int.Parse(lvClients.SelectedItems[0].SubItems[0].Text);

                Client selectedClient = Clients.FirstOrDefault(c => c.ClientID == selectedClientId);

                if (selectedClient != null)
                {
                    OpenClientDetailsForm(selectedClient);
                }
            }
        }

        private void OpenClientDetailsForm(Client client)
        {
            ClientDetailsForm detailsForm = new ClientDetailsForm();
            detailsForm.Owner = this;
            detailsForm.TopLevel = false;
            detailsForm.FormBorderStyle = FormBorderStyle.None;
            detailsForm.Dock = DockStyle.Fill;
            this.Controls.Add(detailsForm);
            detailsForm.BringToFront();
            detailsForm.Show();

            detailsForm.LoadClientDetails(client.ClientID);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvClients.SelectedItems.Count == 0)
            {
                MessageBox.Show("Choose participant!");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this client?", "Delete Client",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning) == DialogResult.OK)
            {
                ListViewItem selectedItem = lvClients.SelectedItems[0];
                Client client = (Client)selectedItem.Tag;

                DeleteClientDB(client);
                DisplayClients();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

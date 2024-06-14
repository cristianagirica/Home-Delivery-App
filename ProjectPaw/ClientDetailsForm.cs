using Project.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProjectPaw
{
    public partial class ClientDetailsForm : Form
    {
        private Client Client;

        public ClientDetailsForm()
        {
            InitializeComponent();
            btnEdit.Click += new EventHandler(btnEdit_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        public void LoadClientDetails(int clientId)
        {
            Console.WriteLine($"Loading client details for client ID: {clientId}");
            Client selectedClient = ClientForm.Clients.FirstOrDefault(c => c.ClientID == clientId);

            if (selectedClient != null)
            {
                UpdateListView(selectedClient);

             
                Client = selectedClient;
                Console.WriteLine($"Client loaded: {Client.Name}");
            }
            else
            {
                MessageBox.Show("Client not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (Client != null)
            {
                OpenEditClientForm(Client);
            }
            else
            {
                MessageBox.Show("No client selected for editing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenEditClientForm(Client client)
        {
            Console.WriteLine($"Opening edit form for client: {client.Name}");
            EditClientForm detailsForm = new EditClientForm(client);
            detailsForm.ClientUpdated += DetailsForm_ClientUpdated;
            detailsForm.TopLevel = false;
            detailsForm.FormBorderStyle = FormBorderStyle.None;
            detailsForm.Dock = DockStyle.Fill;
            this.Controls.Add(detailsForm);
            detailsForm.BringToFront();
            detailsForm.Show();

            detailsForm.LoadClient(client);
        }

        private void DetailsForm_ClientUpdated(object sender, EventArgs e)
        {
            Console.WriteLine("Client details updated, reloading client details...");
            LoadClientDetails(Client.ClientID); 
            RefreshClientListView(); 
        }

        private void RefreshClientListView()
        {
            Console.WriteLine("Refreshing client list view...");
           
            if (this.Owner is ClientForm mainForm)
            {
                mainForm.DisplayClients();
            }
            else
            {
                MessageBox.Show("Owner is not set correctly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateListView(Client client)
        {
            Console.WriteLine($"Updating list view with client: {client.Name}");
            lvCl.Items.Clear();

            ListViewItem item = new ListViewItem(client.ClientID.ToString());
            item.SubItems.Add(client.Name);
            item.SubItems.Add(client.Address);
            item.SubItems.Add(client.Phone);
            item.SubItems.Add(client.Email);

            string ordersString = string.Join(", ", client.Orders.Select(o => o.OrderID.ToString()));
            item.SubItems.Add(ordersString);

            lvCl.Items.Add(item);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

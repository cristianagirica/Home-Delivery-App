using Project.Entitites;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;

namespace ProjectPaw
{
    public partial class EditClientForm : Form
    {
        private Client Client;
        public event EventHandler ClientUpdated;

        public EditClientForm()
        {
            InitializeComponent();
            btnSave.Click += new EventHandler(btnSave_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        public EditClientForm(Client client) : this()
        {
            Client = client;
            LoadClient(client);
            LoadCounties();
        }

        private void LoadCounties()
        {
            List<string> counties = new List<string>
            {
                "Alba", "Arad", "Argeș", "Bacău", "Bihor", "Bistrița-Năsăud", "Botoșani", "Brașov", "Brăila", "Buzău",
                "Caraș-Severin", "Călărași", "Cluj", "Constanța", "Covasna", "Dâmbovița", "Dolj", "Galați", "Giurgiu",
                "Gorj", "Harghita", "Hunedoara", "Ialomița", "Iași", "Ilfov", "Maramureș", "Mehedinți", "Mureș",
                "Neamț", "Olt", "Prahova", "Satu Mare", "Sălaj", "Sibiu", "Suceava", "Teleorman", "Timiș",
                "Tulcea", "Vaslui", "Vâlcea", "Vrancea", "București"
            };

            // Sortează județele alfabetic
            counties.Sort();

            // Adaugă județele sortate în ComboBox
            cbCounty.DataSource = counties;
        }

        public void LoadClient(Client client)
        {
            Client = client;
            if (Client != null)
            {
                tbName.Text = Client.Name;
                tbPhone.Text = Client.Phone;
                tbEmail.Text = Client.Email;
                cbCountry.Checked = true;
                string[] Address = Client.Address.Split(',');
                if (Address.Length >= 4)
                {
                    cbCounty.Text = Address[1].Trim();
                    tbCity.Text = Address[2].Trim();
                    tbAddress.Text = string.Join(", ", Address.Skip(3)).Trim();
                }
                else
                {
                    MessageBox.Show("Client address format is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Client.Name = tbName.Text;
                Client.Phone = tbPhone.Text;
                Client.Email = tbEmail.Text;
                Client.Address = $"{cbCountry.Text.Trim()}, {cbCounty.Text.Trim()}, {tbCity.Text.Trim()}, {tbAddress.Text.Trim()}";

                // Logare pentru depanare
                Console.WriteLine($"Saving Client: {Client.Name}, {Client.Phone}, {Client.Email}, {Client.Address}");

                UpdateClientDB(Client);
                ClientUpdated?.Invoke(this, EventArgs.Empty);

                // Reîncarcă valorile actualizate
                LoadClient(Client);

                MessageBox.Show("Client details updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the client: {ex.Message}");
            }
        }


        private void UpdateClientDB(Client client)
        {
            string query = "UPDATE Client SET Name = @Name, Address = @Address, Phone = @Phone, Email = @Email WHERE ClientId = @ClientId";

            using (SQLiteConnection connection = new SQLiteConnection("Data Source=Database.db"))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", client.Name);
                    command.Parameters.AddWithValue("@Address", client.Address);
                    command.Parameters.AddWithValue("@Phone", client.Phone);
                    command.Parameters.AddWithValue("@Email", client.Email);
                    command.Parameters.AddWithValue("@ClientId", client.ClientID);

                    // Logare pentru depanare
                    Console.WriteLine($"Executing Query: {command.CommandText}");
                    foreach (SQLiteParameter parameter in command.Parameters)
                    {
                        Console.WriteLine($"{parameter.ParameterName}: {parameter.Value}");
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"Rows affected: {rowsAffected}");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}


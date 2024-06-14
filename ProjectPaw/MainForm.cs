
using Project.Entitites;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectPaw
{
    public partial class MainForm : Form
    {
        
        private Dictionary<Type, Form> openForms = new Dictionary<Type, Form>();
        bool sideBarExpand = true;
        public static List<Order> Orders { get; set; }
        public static List<Client> Clients { get; set; }


        public MainForm()
        {
            Orders = new List<Order>();
            Clients = ClientForm.Clients;
            InitializeComponent();
            LoadClients();
        }

        private void LoadClients()
        {
            using (ClientForm clientForm = new ClientForm())
            {
                clientForm.LoadClients(); 
            }
        }

        private void tSideBar_Tick(object sender, EventArgs e)
        {
            if (sideBarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width <= 63)
                {
                    sideBarExpand = false;
                    tSideBar.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width >= 180)
                {
                    sideBarExpand = true;
                    tSideBar.Stop();
                }
            }
        }

        private void btnHam_Click(object sender, EventArgs e)
        {
            tSideBar.Start();
        }

        public void OpenOrCloseForm<T>() where T : Form, new()
        {
            Type formType = typeof(T);

            List<Form> formsToClose = new List<Form>();

            foreach (var openForm in openForms.Values)
            {
                if (openForm.GetType() != formType)
                {
                    formsToClose.Add(openForm);
                }
            }

            foreach (var formToClose in formsToClose)
            {
                formToClose.Close();
            }

            if (openForms.ContainsKey(formType))
            {

                openForms.Remove(formType);
            }
            else
            {

                T newForm = new T();
                newForm.FormClosed += (s, args) => openForms.Remove(formType);
                newForm.MdiParent = this;
                newForm.FormBorderStyle = FormBorderStyle.None;
                newForm.StartPosition = FormStartPosition.CenterScreen;
                newForm.Show();
                openForms.Add(formType, newForm);
            }
        }




        private void btnHome_Click(object sender, EventArgs e)
        {
            OpenOrCloseForm<HomeForm>();
        }

        private void btnClients_Click(object sender, EventArgs e)
        {

            OpenOrCloseForm<ClientForm>();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            OpenOrCloseForm<OrderForm>();
        }

        private void btnInvoices_Click(object sender, EventArgs e)
        {
            OpenOrCloseForm<InvoiceForm>();
        }

        private void btnHome_MouseHover(object sender, EventArgs e)
        {
            btnHome.BackColor = Color.DimGray;
        }

        private void btnHome_MouseLeave(object sender, EventArgs e)
        {
            btnHome.BackColor = Color.Black;
        }

        private void btnClients_MouseHover(object sender, EventArgs e)
        {
            btnClients.BackColor = Color.DimGray;
        }

        private void btnClients_MouseLeave(object sender, EventArgs e)
        {
            btnClients.BackColor = Color.Black;
        }

        private void btnOrders_MouseHover(object sender, EventArgs e)
        {
            btnOrders.BackColor = Color.DimGray;
        }

        private void btnOrders_MouseLeave(object sender, EventArgs e)
        {
            btnOrders.BackColor = Color.Black;
        }

        private void btnInvoices_MouseHover(object sender, EventArgs e)
        {
            btnInvoices.BackColor = Color.DimGray;
        }

        private void btnInvoices_MouseLeave(object sender, EventArgs e)
        {
            btnInvoices.BackColor = Color.Black;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to shutdown the application?", "Confirm Shutdown", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                
                Application.Exit();
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
           
            if (this.GetType() != typeof(MainForm))
            {
                this.Close();
            }
        }


    }
}


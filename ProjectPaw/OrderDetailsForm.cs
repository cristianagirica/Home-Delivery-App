//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using Project.Entitites;

//namespace ProjectPaw
//{
//    public partial class OrderDetailsForm : Form
//    {
//        private Order Order;
//        public OrderDetailsForm()
//        {
//            InitializeComponent();
//        }

//        public void LoadOrderDetails(int orderId)
//        {
//            Console.WriteLine($"Loading order details for order ID: {orderId}");
//            Order selectedOrder = OrderForm.Orders.FirstOrDefault(o => o.OrderID == orderId);

//            if (selectedOrder != null)
//            {
//                UpdateListView(selectedOrder);

//                // Stochează clientul pentru utilizare ulterioară
//                Order = selectedOrder;
//                Console.WriteLine($"Order loaded: {Order.OrderID}");
//            }
//            else
//            {
//                MessageBox.Show("Order not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        private void UpdateListView(Order order)
//        {
//            Console.WriteLine($"Updating list view with client: {Order.OrderID}");
//            lvOr.Items.Clear();

//            ListViewItem item = new ListViewItem(order.OrderID.ToString());
//            item.SubItems.Add(order.ClientID.ToString());
//            item.SubItems.Add(order.OrderDate.ToString());
//            item.SubItems.Add(order.DeliveryDate.ToString());

//            string itemsString = string.Join(", ", order.Items); 
//            item.SubItems.Add(itemsString);

//            string formattedAmount = $"${order.Amount:N2}"; // Formatul va include două zecimale
//            item.SubItems.Add(formattedAmount);

//            item.SubItems.Add(order.Status.ToString());

//            lvOr.Items.Add(item);
//        }
//        private void OrderDetailsForm_Load(object sender, EventArgs e)
//        {

//        }
//    }
//}
using Project.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProjectPaw
{
    public partial class OrderDetailsForm : Form
    {
        private Order Order;

        public OrderDetailsForm()
        {
            InitializeComponent();
        }

        public void LoadOrderDetails(int orderId)
        {
            Console.WriteLine($"Loading client details for client ID: {orderId}");
            Order selectedOrder = OrderForm.Orders.FirstOrDefault(o => o.OrderID == orderId);

            if (selectedOrder != null)
            {
                UpdateListView(selectedOrder);

                Order = selectedOrder;
                Console.WriteLine($"Order loaded: {Order.OrderID}");
            }
            else
            {
                MessageBox.Show("Order not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateListView(Order order)
        {
            if (order == null)
            {
                Console.WriteLine("Order is null. Cannot update list view.");
                return;
            }

            Console.WriteLine($"Updating list view with order: {order.OrderID}");
            lvOr.Items.Clear();

            ListViewItem item = new ListViewItem(order.OrderID.ToString());
            item.SubItems.Add(order.ClientID.ToString());
            item.SubItems.Add(order.OrderDate.ToString());
            item.SubItems.Add(order.DeliveryDate.ToString());

            string itemsString = string.Join(", ", order.Items);
            item.SubItems.Add(itemsString);

            string formattedAmount = $"${order.Amount:N2}"; 
            item.SubItems.Add(formattedAmount);

            item.SubItems.Add(order.Status.ToString());

            lvOr.Items.Add(item);
        }


      
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

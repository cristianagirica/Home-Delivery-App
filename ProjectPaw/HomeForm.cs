using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Project.Entitites;
using static Project.Entitites.Order;

namespace ProjectPaw
{
    public partial class HomeForm : Form
    {
        private string ConnectionString = "Data Source=database.db";
        public static List<Order> Orders = new List<Order>();

        public HomeForm()
        {
            InitializeComponent();
          
            Panel chartPanel = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(919, 600),
                BorderStyle = BorderStyle.FixedSingle
            };
            chartPanel.Paint += new PaintEventHandler(this.ChartPanel_Paint);
            this.Controls.Add(chartPanel);

            LoadOrders();
        }

       

        private void ChartPanel_Paint(object sender, PaintEventArgs e)
        {
            Dictionary<string, int> productCounts = GetProductCounts();

            if (productCounts.Count == 0)
            {
                e.Graphics.DrawString("No data available to display.", new Font("Arial", 16), Brushes.Black, new PointF(10, 10));
                return;
            }

            float total = productCounts.Values.Sum();
            float currentAngle = 0;

            Rectangle rect = new Rectangle(10, 10, 300, 300);
            Random rand = new Random();
            Dictionary<string, Color> productColors = new Dictionary<string, Color>();

            foreach (var product in productCounts)
            {
                float sweepAngle = (product.Value / total) * 360;
                Color color;

                if (!productColors.TryGetValue(product.Key, out color))
                {
                  
                    color = Color.FromArgb(rand.Next(100, 256), rand.Next(0, 100), rand.Next(100, 256));
                    productColors[product.Key] = color;
                }

                Brush brush = new SolidBrush(color);

                e.Graphics.FillPie(brush, rect, currentAngle, sweepAngle);
                e.Graphics.DrawPie(Pens.Black, rect, currentAngle, sweepAngle);

                currentAngle += sweepAngle;
            }

           
            int legendX = 320;
            int legendY = 10;
            foreach (var product in productCounts)
            {
                Color color = productColors[product.Key];
                Brush brush = new SolidBrush(color);
                e.Graphics.FillRectangle(brush, legendX, legendY, 20, 20);
                e.Graphics.DrawRectangle(Pens.Black, legendX, legendY, 20, 20);
                e.Graphics.DrawString($"{product.Key} - {product.Value} ({(product.Value / total) * 100:F2}%)", new Font("Arial", 10), Brushes.Black, new PointF(legendX + 25, legendY));

                legendY += 30;
            }
        }

        private Dictionary<string, int> GetProductCounts()
        {
          
            Dictionary<string, int> productCounts = new Dictionary<string, int>();

            foreach (var order in Orders)
            {
                foreach (var item in order.Items)
                {
                    if (productCounts.ContainsKey(item))
                    {
                        productCounts[item]++;
                    }
                    else
                    {
                        productCounts[item] = 1;
                    }
                }
            }

            return productCounts;
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
    }
}

using System;
using System.Collections.Generic;

namespace Project.Entitites
{
    public class Order
    {
        public enum DeliveryStatus
        {
            Pending,
            InTransit,
            OutForDelivery,
            Delivered,
            Cancelled
        }
        private static readonly Dictionary<string, double> itemPrices = new Dictionary<string, double>
        {
            { "Red Roses Bouquet", 250.00 },
            { "Pink Peonies Bouquet", 299.00 },
            { "Lavender Bouquet", 220.99 },
            { "Sunflower Bouquet", 190.50 },
            { "Tulip Bouquet", 240.99 },
            { "Orchid Arrangement", 350.40 },
            { "Daisy Bouquet", 180.20 },
            { "Mixed Flowers Bouquet", 270.99 },
            { "White Lilies Bouquet", 315.99 },
            { "Blue Hydrangeas Bouquet", 260.99 },
            { "Carnation Bouquet", 210.99 },
            { "Calla Lily Bouquet", 330.99 },
            { "Gerbera Daisy Bouquet", 230.99 },
            { "Potted Orchid", 390.99 },
            { "Peony Arrangement", 375.99 },
            { "Rose and Lily Bouquet", 286.99 },
            { "Wildflower Bouquet", 203.99 },
            { "Cactus Plant", 157.99 },
            { "Succulent Garden", 180.99 },
            { "Bonsai Tree", 451.99 }
        };

       
        public int OrderID { get; set; }
        public int ClientID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public List<string> Items { get; set; } = new List<string>(); 
        public double Amount { get; set; }
        public DeliveryStatus Status { get; set; } = DeliveryStatus.Pending;

        public Order()
        {
            OrderDate = DateTime.Now;
            DeliveryDate = DateTime.Now;
        }

        public Order(int clientID, DateTime orderDate, DateTime deliveryDate, List<string> items, double amount, DeliveryStatus status)
        {
            ClientID = clientID;
            OrderDate = orderDate;
            DeliveryDate = deliveryDate;
            Items = items ?? new List<string>(); 
            Amount = amount;
            Status = status;
        }

        public Order(int orderId, int clientID, DateTime orderDate, DateTime deliveryDate, List<string> items, double amount, DeliveryStatus status)
        {
            OrderID = orderId;
            ClientID = clientID;
            OrderDate = orderDate;
            DeliveryDate = deliveryDate;
            Items = items ?? new List<string>();
            Amount = amount;
            Status = status;
        }

        public double CalculateAmount()
        {
            double totalAmount = 0;
            foreach (string s in this.Items)
            {
                if (itemPrices.ContainsKey(s))
                {
                    totalAmount += itemPrices[s];
                }
                else
                {
                    Console.WriteLine($"Warning: Price for item '{s}' not found.");
                }
            }
            return totalAmount;
        }

        public string ItemsToString()
        {
            string st = "";
            foreach (string s in this.Items)
            {
                st = st + ", " + s;
            }
            return st;
        }
    }
}

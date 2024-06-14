using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entitites
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public int OrderID { get; set; }
        public int ClientID { get; set; }
        public DateTime InvoiceDate { get; set; }
        public double Amount { get; set; }

        public Invoice()
        {
            InvoiceDate = DateTime.Now;
        }
        public Invoice(int orderID, int clientID, DateTime invoiceDate, double amount)
        {
            OrderID = orderID;
            ClientID = clientID;
            InvoiceDate = invoiceDate;
            Amount = amount;
        }

        public Invoice(int invoiceID,int orderID, int clientID, DateTime invoiceDate, double amount)
        {
            InvoiceID = invoiceID;
            OrderID = orderID;
            ClientID = clientID;
            InvoiceDate = invoiceDate;
            Amount = amount;
        }
    }
}

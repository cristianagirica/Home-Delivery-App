using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entitites
{
    public class Client
    {
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Order> Orders { get; set; }

        public Client()
        {
        }
        public Client(string name, string address, string phone, string email)
        {
            Name = name;
            Address = address;
            Phone = phone;
            Email = email;
            Orders = new List<Order>();
        }

        public Client(int id, string name, string address, string phone, string email, List<Order> orders)
        {
            ClientID = id;
            Name = name;
            Address = address;
            Phone = phone;
            Email = email;
            Orders = orders;
        }

    }
}


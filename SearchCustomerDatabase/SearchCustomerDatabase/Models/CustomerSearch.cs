using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NorthwindEntity;

namespace SearchCustomerDatabase.Models
{
    public class CustomerSearch
    {
        public double zwischenSpeicher { get; set; }
        public decimal wDiscount { get; set; }
        public decimal woDiscount { get; set; }
        public double sumWDiscount { get; set; }
        public double sumWoDiscount { get; set; }
        public double discount { get; set; }
        public double totalDiscount { get; set; }


        public string oldCustomerSearch { get; set; }

        public IQueryable<Customer> customer { get; set; }
        public IQueryable<Order> orders { get; set; }
        public IQueryable<Order_Detail> orderDetails { get; set; }

        public double totalPrice { get; set; }
    }
}
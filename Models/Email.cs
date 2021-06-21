using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electric.Models
{
    public class Email
    {
        public Customer customer { get; set; }

        public string emailName { get; set; }

        public Product product { get; set; }

        public Order order { get; set; }

    }
}
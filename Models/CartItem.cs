﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electric.Models
{
    [Serializable]

    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string UserName { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Web.Models
{
    public class UpdateCartViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PsdigitalEcommerceTask.ViewModels
{
    public class CartViewModel
    {
        public  List<Models.Cart> Carts { get; set; }
        public decimal SubTotal { get; set; }
        public int ItemsCount { get; set; } = 0;
    }
}
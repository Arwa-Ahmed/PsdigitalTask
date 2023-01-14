using PsdigitalEcommerceTask.Interfaces;
using PsdigitalEcommerceTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PsdigitalEcommerceTask.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(PsdigitalEcommerceTaskContext context) : base(context) { }
    }
}
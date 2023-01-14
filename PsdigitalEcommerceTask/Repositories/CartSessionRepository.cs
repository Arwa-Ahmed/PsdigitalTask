using PsdigitalEcommerceTask.Interfaces;
using PsdigitalEcommerceTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PsdigitalEcommerceTask.Repositories
{
    public class CartSessionRepository : GenericRepository<CartSession>, ICartSessionRepository
    {
        public CartSessionRepository(PsdigitalEcommerceTaskContext context) : base(context) { }
    }
}
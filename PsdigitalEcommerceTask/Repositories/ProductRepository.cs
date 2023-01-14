using PsdigitalEcommerceTask.Interfaces;
using PsdigitalEcommerceTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PsdigitalEcommerceTask.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(PsdigitalEcommerceTaskContext context) : base(context) { }
    }
}
using PsdigitalEcommerceTask.Interfaces;
using PsdigitalEcommerceTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PsdigitalEcommerceTask.Repositories
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        public AdminRepository(PsdigitalEcommerceTaskContext context) : base(context) { }
    }
}
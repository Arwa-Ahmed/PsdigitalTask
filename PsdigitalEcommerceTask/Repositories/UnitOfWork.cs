using PsdigitalEcommerceTask.Interfaces;
using PsdigitalEcommerceTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PsdigitalEcommerceTask.Repositories
{
    public class UnitOfWork : IUnitOfWork , IDisposable
    {
        private readonly PsdigitalEcommerceTaskContext context;
        public IUserRepository Users { get; private set; }

        public IProductRepository Products { get; private set; }

        public IAdminRepository Admins { get; private set; }

        public ICartRepository Carts { get; private set; }

        public ICartSessionRepository CartSessions { get; private set; }

        public UnitOfWork(PsdigitalEcommerceTaskContext context)
        {
            this.context = context;
            Users = new UserRepository(this.context);
            Products = new ProductRepository(this.context);
            Admins = new AdminRepository(this.context);
            Carts = new CartRepository(this.context);
            CartSessions = new CartSessionRepository(this.context);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsdigitalEcommerceTask.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IProductRepository Products { get; }
        IAdminRepository Admins { get; }
        ICartRepository Carts { get; }
        ICartSessionRepository CartSessions { get; }

        void Save();

        void Dispose();
    }
}

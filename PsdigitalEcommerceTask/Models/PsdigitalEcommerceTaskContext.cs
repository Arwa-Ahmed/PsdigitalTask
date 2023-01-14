using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PsdigitalEcommerceTask.Models
{
    public partial class PsdigitalEcommerceTaskContext : DbContext
    {
        public PsdigitalEcommerceTaskContext()
            : base("name=PsdigitalEcommerceTaskContext")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartSession> CartSessions { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartSession>()
                .HasMany(e => e.Carts)
                .WithRequired(e => e.CartSession)
                .HasForeignKey(e => e.SessionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Carts)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.CartSessions)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}

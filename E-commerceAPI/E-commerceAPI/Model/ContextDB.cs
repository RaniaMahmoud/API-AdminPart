using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using E_commerceAPI.DTO;

namespace E_commerceAPI.Model
{
    public class ContextDB: IdentityDbContext<AppUser>
    {
        public ContextDB() : base()//onconfigu
        {

        }
        public ContextDB(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> Products { set; get; }
        public DbSet<Category> Category { set; get; }
        public DbSet<Store> Store { set; get; }
        public DbSet<Address> Address { set; get; }
        public DbSet<Branche> Branche { set; get; }
        public DbSet<Phone> Phone { set; get; }
        public DbSet<ProductQuantity> ProductQuantities { set; get; }
        public DbSet<Card> Cards { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<Admin> Admin { set; get; }
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}

using System.Data.Entity;
using DataLayer.DAL.Entities;

namespace DataLayer.DAL.Context
{
    public class DataContext : DbContext
    {
        public DataContext(string name) : base(name)
        {
            Database.SetInitializer(new DataContextInit());
        }

        public DbSet<CartItem> Cart { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Price> Price { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Settings> Settings { get; set; }
    }
}

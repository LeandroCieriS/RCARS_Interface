using Microsoft.EntityFrameworkCore;
using RCARS.Interface.Models;

namespace RC_QueryService.Conection
{
    public class RCDatabase : DbContext
    {
        public RCDatabase(DbContextOptions options) : base(options) { }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

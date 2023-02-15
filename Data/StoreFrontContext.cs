using SimpleStoreFront.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SimpleStoreFront.Data
{
    public class StoreFrontContext : IdentityDbContext<StoreUser>
    {
        public StoreFrontContext(DbContextOptions<StoreFrontContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasData(new Order()
                {
                    Id = 1,
                    OrderDate = DateTime.UtcNow,
                    OrderNumber = "12345"
                });
        }
    }
}

//private readonly IConfiguration _config;

//public StoreFrontContext(IConfiguration config)
//{
//    _config = config;
//}
//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//{
//    base.OnConfiguring(optionsBuilder);

//    optionsBuilder.UseSqlServer(_config["ConnectionStrings:StoreFrontContextDb"]);
//}
//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    base.OnModelCreating(modelBuilder);

//    modelBuilder.Entity<Order>()
//        .HasData(new Order()
//        {
//            Id = 1,
//            OrderDate = DateTime.UtcNow,
//            OrderNumber = "12345"
//        });
//}



using SimpleStoreFront.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace SimpleStoreFront.Data
{
    public class StoreFrontContext : DbContext
    {
        public StoreFrontContext(DbContextOptions<StoreFrontContext> options) : base(options)
        {

        }

        //private readonly IConfiguration _config;

        //public StoreFrontContext(IConfiguration config)
        //{
        //    _config = config;
        //}
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);

        //    optionsBuilder.UseSqlServer(_config["ConnectionStrings:StoreFrontContextDb"]);
        //}

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

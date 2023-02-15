using Microsoft.AspNetCore.Identity;
using SimpleStoreFront.Data.Entities;
using System.Text.Json;

namespace SimpleStoreFront.Data
{
    public class StoreSeeder
    {
        private readonly StoreFrontContext _ctx;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<StoreUser> _userManager;

        public StoreSeeder(StoreFrontContext ctx, IWebHostEnvironment env, UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _env = env;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            _ctx.Database.EnsureCreated();

            StoreUser user = await _userManager.FindByEmailAsync("vance@store.com");
            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Vance",
                    LastName = "Trevino",
                    Email = "vance@store.com",
                    UserName = "vance@store.com"
                };

                var results = await _userManager.CreateAsync(user, "P@ssw0rd321!");

                if (results != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }

            if (!_ctx.Products.Any())
            {
                // Need to create the Sample Data
                var filePath = Path.Combine(_env.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);

                _ctx.Products.AddRange(products);

                var order = _ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();

                if (order == null)
                {
                    order = new Order()
                    {
                        OrderDate = DateTime.Today,
                        OrderNumber = "1000",
                        };
                    order.User = user;
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    };
                };

                _ctx.Orders.Add(order);

                _ctx.SaveChanges();
            }
        }
    }
}

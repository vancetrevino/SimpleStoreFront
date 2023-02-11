﻿using SimpleStoreFront.Data.Entities;
using System.Text.Json;

namespace SimpleStoreFront.Data
{
    public class StoreSeeder
    {
        private readonly StoreFrontContext _ctx;
        private readonly IWebHostEnvironment _env;

        public StoreSeeder(StoreFrontContext ctx, IWebHostEnvironment env)
        {
            _ctx = ctx;
            _env = env;
        }

        public void Seed()
        {
            _ctx.Database.EnsureCreated();

            if (!_ctx.Products.Any())
            {
                // Need to create the Sample Data
                var filePath = Path.Combine(_env.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);

                _ctx.Products.AddRange(products);

                var order = new Order()
                {
                    OrderDate = DateTime.Today,
                    OrderNumber = "1000",
                    Items = new List<OrderItem>
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };

                _ctx.Orders.Add(order);

                _ctx.SaveChanges();
            }
        }
    }
}

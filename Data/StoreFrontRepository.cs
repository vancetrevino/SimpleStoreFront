using Microsoft.EntityFrameworkCore;
using SimpleStoreFront.Data.Entities;

namespace SimpleStoreFront.Data
{
    public class StoreFrontRepository : IStoreFrontRepository
    {
        private readonly StoreFrontContext _ctx;
        private readonly ILogger<StoreFrontRepository> _logger;

        public StoreFrontRepository(StoreFrontContext ctx, ILogger<StoreFrontRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model); 
        }

        public IEnumerable<Order> GetAllOrderByUser(string username, bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                            .Where(o => o.User.UserName == username)
                            .Include(o => o.Items)
                            .ThenInclude(i => i.Product)
                            .ToList();
            }
            else
            {
                return _ctx.Orders
                            .Where(o => o.User.UserName == username)
                            .ToList();
            }
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                            .Include(o => o.Items)
                            .ThenInclude(i => i.Product)
                            .ToList();
            }
            else
            {
                return _ctx.Orders.ToList();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            _logger.LogInformation("GetAllProducts was logged");
            return _ctx.Products
                        .OrderBy(p => p.Title)
                        .ToList();
        }

        public Order GetOrderById(string username, int id)
        {
            return _ctx.Orders
                        .Include(o => o.Items)
                        .ThenInclude(i => i.Product)
                        .Where(o => o.Id == id && o.User.UserName == username)
                        .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products
                    .OrderBy(p => p.Category == category)
                    .ToList();
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}

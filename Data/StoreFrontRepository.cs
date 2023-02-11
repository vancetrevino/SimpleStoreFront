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

        public IEnumerable<Product> GetAllProducts()
        {
            _logger.LogInformation("GetAllProducts was logged");
            return _ctx.Products
                    .OrderBy(p => p.Title)
                    .ToList();
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

using SimpleStoreFront.Data.Entities;

namespace SimpleStoreFront.Data
{
    public interface IStoreFrontRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        bool SaveAll();
    }
}
using Microsoft.AspNetCore.Mvc;
using SimpleStoreFront.Data.Entities;

namespace SimpleStoreFront.Data
{
    public interface IStoreFrontRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        
        IEnumerable<Order> GetAllOrders(bool includeItems);
        Order GetOrderById(int id);

        bool SaveAll();
        void AddEntity(object model);
    }
}
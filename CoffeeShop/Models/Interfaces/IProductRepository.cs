using System;
namespace CoffeeShop.Models.Interfaces
{
	public interface IProductRepository
    {
        // returns list of products
        IEnumerable<Product> GetAllProducts();

        // returns list of trending products
        IEnumerable<Product> GetTrendingProducts();

        // returns list of products
        Product? GetProductById(int id);
    }
}


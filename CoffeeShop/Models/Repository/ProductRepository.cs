using System;
using CoffeeShop.Data;
using CoffeeShop.Models.Interfaces;

namespace CoffeeShop.Models.Services
{
    public class ProductRepository : IProductRepository
    {
        private CoffeeShopDbContext dbContext;

        public ProductRepository(CoffeeShopDbContext dbContext)
        {
            this.dbContext = dbContext; 
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return dbContext.Products; 
        }

        public Product? GetProductById(int id)
        {
            return dbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetTrendingProducts()
        {
            // lambda expression to filter all trending products 
            return dbContext.Products.Where(p => p.IsTrendingProduct); 
        }
    }
}


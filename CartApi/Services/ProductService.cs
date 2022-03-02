using CartApi.Data.Models;
using CartApi.Services.Interfaces;

namespace CartApi.Services
{
    public class ProductService : IProductService
    {
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Shampoo", Price = 12 },
            new Product { Id = 2, Name = "Soap", Price = 5 },
            new Product { Id = 3, Name = "Bread", Price = 1 },
            new Product { Id = 4, Name = "ShavingCream", Price = 32 },
            new Product { Id = 5, Name = "HandSanitizer", Price = 78 },
            new Product { Id = 6, Name = "Cookie", Price = 0.2M },
            new Product { Id = 7, Name = "Oil", Price = 2.9M },
            new Product { Id = 8, Name = "Pencil", Price = 0.9M },
            new Product { Id = 9, Name = "Mug", Price = 9.9M },
            new Product { Id = 10, Name = "Phone", Price = 299.99M },
            new Product { Id = 11, Name = "Laptop", Price = 1000 },
        };

        public async Task<bool> AnyProductById(long id)
        {
            bool exists = _products.Exists(x => x.Id == id);

            return exists;
        }

        public async Task<Product?> GetProductById(long id)
        {
            Product? product = _products.FirstOrDefault(x => x.Id == id);

            return product;
        }
    }
}
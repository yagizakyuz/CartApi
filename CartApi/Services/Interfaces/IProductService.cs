using CartApi.Data.Models;

namespace CartApi.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductById(long id);

        Task<bool> AnyProductById(long id);
    }
}

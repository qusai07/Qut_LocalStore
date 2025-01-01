using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project1.Controllers;
using Project1.Models;

namespace Project1.Repository.IRepository
{
    public interface IProductRepository
    {
        Task<IdentityResult> AddProductAsync(Product product);

       // Task AddProductAsync(Product product);
        Task EditProductAsync(Product product);
        Task<bool> DeleteAsync(int productId);
        Task<List<Product>> GetAllProductsAsync();
        public Product GetById(int id);

    }
}

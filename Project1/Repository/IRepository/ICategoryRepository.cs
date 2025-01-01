using Project1.Models;

namespace Project1.Repository.IRepository
{
    public interface ICategoryRepository
    {
        Task AddCategoryAsync(Product product);
        Task EditCategoryAsync(Product product);
        Task DeleteCategoryAsync(int productid);
    }
}

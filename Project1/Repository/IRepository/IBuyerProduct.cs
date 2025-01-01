using Microsoft.AspNetCore.Identity;
using Project1.Models;

namespace Project1.Repository.IRepository
{
    public interface IBuyerProduct
    {
        Task BuyProductAsync(TheBuyer theBuyer );
        //Task<IdentityResult> BuyProductAsync(TheBuyer theBuyer);

        Task<List<TheBuyer>> GetAllRequestProductsAsync();
    }
}

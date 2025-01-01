using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Models;
using Project1.Repository.IRepository;

namespace Project1.Repository.Repository
{
    public class BuyerProduct : IBuyerProduct
    {
        //private readonly List<Product> _purchasedProducts;
        private readonly List<TheBuyer> _buyers = new List<TheBuyer>();
        private readonly AppDbContext _appDbContext;


        public BuyerProduct(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            //_purchasedProducts = new List<Product>();
            _buyers = new List<TheBuyer>();

        }

        public async Task<List<TheBuyer>> GetAllRequestProductsAsync()
        {
            return await _appDbContext.theBuyer.ToListAsync();
        }

        async Task IBuyerProduct.BuyProductAsync(TheBuyer theBuyer)
        {
            _appDbContext.theBuyer.Add(theBuyer);
            await _appDbContext.SaveChangesAsync();
            _buyers.Add(theBuyer);
           
        }
    }
}

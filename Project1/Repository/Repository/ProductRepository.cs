using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project1.Controllers;
using Project1.Data;
using Project1.Models;
using Project1.Repository.IRepository;
using System;

namespace Project1.Repository.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;


        public ProductRepository(AppDbContext appDb)
        {
            _appDbContext = appDb;
        }

        public async Task EditProductAsync(Product product)
        {
            _appDbContext.products.Update(product);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IdentityResult> AddProductAsync(Product product)
        {
            _appDbContext.products.Add(product);
             await _appDbContext.SaveChangesAsync();
            return IdentityResult.Success;

        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _appDbContext.products.ToListAsync();
        }

        public async Task <bool> DeleteAsync(int productid)
        {
            var product = await _appDbContext.products.FindAsync(productid);
            if (product != null)
            {
                _appDbContext.products.Remove(product);
                await _appDbContext.SaveChangesAsync();     
                return true;

            }
            return false;
        }

        public Product GetById(int id)
        {
            return _appDbContext.products.Find(id);
        }
    }
}

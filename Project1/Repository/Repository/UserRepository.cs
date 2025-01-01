using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Models;
using Project1.Repository.IRepository;
using System;

namespace Project1.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly AppDbContext _appDbContext;
        public UserRepository(UserManager<User> _UserManager, SignInManager<User> _signInManager, RoleManager<IdentityRole<int>> _roleManager ,AppDbContext _appDbContext)
        {
            this._UserManager = _UserManager;
            this._signInManager = _signInManager;
            this._roleManager = _roleManager;
            this._appDbContext =_appDbContext;

        }

        public async Task <IdentityResult>AddUserAsync(User User , string password)
        {
            return await _UserManager.CreateAsync(User , password);
        }

        //public async Task DeleteUserAsync(string Userid)
        //{
        //    var User = await GetUserAsync(Userid);
        //    if (User != null)
        //    {
        //        var resulut = await _UserManager.DeleteAsync(User);
        //    }

        //}

        public async Task DeleteUserAsync(User User)
        {

            var result = await _UserManager.DeleteAsync(User);

        }

        public async Task<User> GetUserAsync(string username)
        {
            return await _UserManager.FindByNameAsync(username);
        }

        public async Task UpdateUserAsync(User User)
        {
            var result = await _UserManager.UpdateAsync(User);

        }
        public async Task SignInAsync(User User)
        {
            await _signInManager.SignInAsync(User, false);
        }
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> AddUserToRoleAsync(User user, string role)
        {
            return await _UserManager.AddToRoleAsync(user, role);
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _appDbContext.Users.ToListAsync();
        }
    }
}

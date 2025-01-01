using Microsoft.AspNetCore.Identity;
using Project1.Controllers;
using Project1.Models;

namespace Project1.Repository.IRepository
{
    public interface IUserRepository
    {
        Task <IdentityResult>AddUserAsync(User User, string password);
        Task<IdentityResult> AddUserToRoleAsync(User user, string role);

        Task UpdateUserAsync(User User);
        Task DeleteUserAsync(User User);
        Task<User> GetUserAsync(string username);
        Task SignInAsync(User user);
        Task SignOutAsync();
        Task<List<User>> GetAllUsersAsync();


    }
} 

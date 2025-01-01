using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Models;
using Project1.Repository.IRepository;
using Project1.Repository.Repository;
using Project1.ViewModels;
using Project1.ViewModels.ProductView;
using System.Runtime.InteropServices;

namespace Project1.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<User> usermanger;
        private readonly SignInManager<User> signInManager;
        private readonly IUserRepository _userRepository;
        private readonly IServiceProvider _provider;
        private readonly AppDbContext _appDbContext;
        private readonly RoleManager<IdentityRole<int>> _roleManager;




        public AdminController(UserManager<User> usermanger,SignInManager<User> signInManager,IUserRepository userRepository , IServiceProvider serviceProvider, AppDbContext appDbContext, RoleManager<IdentityRole<int>> roleManager)
        {
            this.usermanger = usermanger;
            this.signInManager = signInManager;
            _userRepository = userRepository;
            _provider = serviceProvider;
            _appDbContext = appDbContext;
            _roleManager = roleManager;

        }

        [Authorize(Roles = "Admin")]

        public IActionResult CreateOwner()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOwner(CreateOwner model)
        {
            if (ModelState.IsValid)
            {
                User owner = new User()
                {
                    UserName = model.Name,
                    Email = model.Email,
                    NormalizedUserName = model.NormalizedUserName,
                    PhoneNumber=model.PhoneNumebr,
                };
               var  result = await _userRepository.AddUserAsync(owner, model.Password);
               var role = await usermanger.AddToRoleAsync(owner ,"User");
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");

                }
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Home()
        {
        
            return View();
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Table()
        {
            var user = await _userRepository.GetAllUsersAsync();
            ListOfInfo listOfInfo = new ListOfInfo() { users = user };
            return View(listOfInfo);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task <IActionResult> AdminProfile()
        {
            var user = await usermanger.FindByNameAsync(User.Identity.Name);
            return View(user);
        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async  Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = await usermanger.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var role = await usermanger.GetRolesAsync(user);
                    if (role.Contains("Admin"))
                    {
                        var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            }
            return View(model);
        }

        public async Task<IActionResult> signout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



    }
}

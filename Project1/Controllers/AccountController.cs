using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project1.Data;
using Project1.Models;
using Project1.Repository.IRepository;
using Project1.Repository.Repository;
using Project1.ViewModels;

namespace Project1.Controllers
{
    public class AccountController : Controller
    {
    private readonly IUserRepository UserRepository;
    private readonly IProductRepository _productRepository;
    private readonly AppDbContext _appDbContext;
    private readonly UserManager<User> _UserManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

        public AccountController(IUserRepository userRepository, IProductRepository productRepository, AppDbContext appDbContext, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<int>> roleManager)
        {
            UserRepository = userRepository;
            _productRepository = productRepository;
            _appDbContext = appDbContext;
            _UserManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]     
        [AllowAnonymous]

        public IActionResult Login()
        {
            ViewBag.CurrentPage = "login";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var role = await _UserManager.GetRolesAsync(user);
                    if (await _UserManager.IsInRoleAsync(user, "Admin"))
                    {
                        var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
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
        [Authorize]

        public async Task<IActionResult> signout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Extensions;
using Project1.Models;
using Project1.Repository.IRepository;
using Project1.Repository.Repository;
using Project1.ViewModels;
using Project1.ViewModels.ProductView;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;

namespace Project1.Controllers
{
   
    public class OwnerController : Controller
    {
        private readonly IUserRepository UserRepository;
        private readonly IProductRepository _productRepository;
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IBuyerProduct _buyerProduct;


        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.CurrentPage = "login";
            return View();
        }
        public OwnerController(IUserRepository UserRepository, UserManager<User> UserManager, SignInManager<User> signInManager, RoleManager<IdentityRole<int>> roleManager, IProductRepository productRepository, IWebHostEnvironment webHostEnvironment , AppDbContext appDbContext , IBuyerProduct buyerProduct)
        {
            this.UserRepository = UserRepository;
            _UserManager = UserManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
            _appDbContext = appDbContext;
            _buyerProduct = buyerProduct;
        }

        public async Task<IActionResult> signout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = await UserRepository.GetUserAsync(model.UserName);
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Profile", "Owner");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await UserRepository.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound("Error");
            }
            var UserData = new CreateOwner
            {
                Email = user.Email,
                Name = user.UserName,
                NormalizedUserName = user.NormalizedUserName,
                PhoneNumebr = user.PhoneNumber
            };
            return View(UserData);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> myproduct()
        {
            var product = await _productRepository.GetAllProductsAsync();
            ListOfInfo listOfInfo = new ListOfInfo() { products = product };
            return View(listOfInfo);

        }
        [HttpGet]
        [Authorize]
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public IActionResult EditProduct()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AddProduct(CreateProducts model)
        {
            if (ModelState.IsValid)
            {
                var owner = await _UserManager.FindByNameAsync(User.Identity.Name);
                var product = new Product
                {
                    UserId = owner.Id,
                    Name = model.Name,
                    Description = model.Description,
                    Price = int.Parse(model.Price),
                    Image = FileExtensions.ConvertFileToString(model.formFile, _webHostEnvironment),

                };
                var result = await _productRepository.AddProductAsync(product);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Failed");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProductAsync(Product product)
        {
            await _productRepository.EditProductAsync(product);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> DeleteProduct(int productid)
        {
            _productRepository.DeleteAsync(productid);
            return RedirectToAction("myproduct", "Owner");

        }

        [Authorize]

        public async Task  <IActionResult> CustomeRequests(int id)
        {
            var product = await   _appDbContext.theBuyer.ToListAsync();
            return View(product);
        }


    }

}

using Microsoft.AspNetCore.Mvc;
using Project1.Extensions;
using Project1.Models;
using Project1.Repository.IRepository;
using Project1.ViewModels;
using Project1.ViewModels.ProductView;
using System.Diagnostics;

namespace Project1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private  readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IBuyerProduct _buyerProduct;


        public HomeController(ILogger<HomeController> logger ,IProductRepository productRepository  , IBuyerProduct buyerProduct )
        {
            _logger = logger;
            _productRepository = productRepository;
            _buyerProduct = buyerProduct;
        }
       
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.CurrentPage = "index";
            return View();
        }
     

        public async  Task <IActionResult> MenProduct()
        {
            var product = await _productRepository.GetAllProductsAsync();
            ListOfInfo listOfInfo = new ListOfInfo()
            { products = product };
           
            return View(listOfInfo);
        }
        public IActionResult WomenProduct()
        {
            return View();
        }
        public IActionResult KidsProduct()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.CurrentPage = "contact";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult BuyProduct(string productName, decimal price)
        {

            var model = new BuyProductModel
            {
                ProductName = productName,
                ProductPrice = price
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> BuyProduct(BuyProductModel model , string productName, double price )
        {
 
            if (ModelState.IsValid)
            {
                var theBuyer = new TheBuyer
                {
                    Name = model.NameUser,
                    Email = model.EmailBuyer,
                    ProductDescription = model.ProductDescription,
                    PhoneNumber = model.NumberBuyer,
                    ProductName = productName,
                    ProductPrice = price.ToString()
                };
                await _buyerProduct.BuyProductAsync(theBuyer);

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}

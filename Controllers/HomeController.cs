// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using MiniShop.Models;
// using MiniShop.Services;
//
// namespace MiniShop.Controllers;

// public class HomeController : Controller
// {
//     private readonly ApplicationContext _context;
//     private readonly IBaseService<Product> _productService;
//     private readonly ILogger<HomeController> _logger;
//
//     public HomeController(ApplicationContext context, ILogger<HomeController> logger, IBaseService<Product> productService)
//     {
//         _context = context;
//         _logger = logger;
//         _productService = productService;
//     }
//
//     public IActionResult Index()
//     {
//         IEnumerable<Product> products = _context.Products.Include(product => product.Image).ToList();
//         return View(products);
//     }
//     
// }
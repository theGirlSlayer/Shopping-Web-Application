using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Estore.Models;
using System.Web;
using Microsoft.AspNetCore.Http;
namespace Estore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            Account sessionUSer = (Account) DataProvider.ByteArrayToObject(HttpContext.Session.Get("SessionUser"));
            List<ProductInCart> sessionProductInCart = Product.GetProductsInCartByUIntList((List<uint>)DataProvider.ByteArrayToObject(HttpContext.Session.Get("cart")));
            ViewData.Add("sessionProductInCart", sessionProductInCart);
            return View(sessionUSer);
        }
        public IActionResult Blog()
        {
            Account sessionUSer = (Account) DataProvider.ByteArrayToObject(HttpContext.Session.Get("SessionUser"));
            List<ProductInCart> sessionProductInCart = Product.GetProductsInCartByUIntList((List<uint>)DataProvider.ByteArrayToObject(HttpContext.Session.Get("cart")));
            ViewData.Add("sessionProductInCart", sessionProductInCart);
            return View(sessionUSer);
        }
        public IActionResult CheckOut()
        {
            Account sessionUSer = (Account) DataProvider.ByteArrayToObject(HttpContext.Session.Get("SessionUser"));
            List<ProductInCart> sessionProductInCart = Product.GetProductsInCartByUIntList((List<uint>)DataProvider.ByteArrayToObject(HttpContext.Session.Get("cart")));
            ViewData.Add("sessionProductInCart", sessionProductInCart);
            if (sessionUSer == null)
            {
                return Redirect("/User/Login");
            }
            return View(sessionUSer);
        }
        public IActionResult Contact()
        {
            Account sessionUSer = (Account) DataProvider.ByteArrayToObject(HttpContext.Session.Get("SessionUser"));
            List<ProductInCart> sessionProductInCart = Product.GetProductsInCartByUIntList((List<uint>)DataProvider.ByteArrayToObject(HttpContext.Session.Get("cart")));
            ViewData.Add("sessionProductInCart", sessionProductInCart);
            return View(sessionUSer);
        }
        public IActionResult BlogDetails()
        {
            Account sessionUSer = (Account) DataProvider.ByteArrayToObject(HttpContext.Session.Get("SessionUser"));
            List<ProductInCart> sessionProductInCart = Product.GetProductsInCartByUIntList((List<uint>)DataProvider.ByteArrayToObject(HttpContext.Session.Get("cart")));
            ViewData.Add("sessionProductInCart", sessionProductInCart);
            return View(sessionUSer);
        }
        public IActionResult Faq()
        {
            Account sessionUSer = (Account) DataProvider.ByteArrayToObject(HttpContext.Session.Get("SessionUser"));
            List<ProductInCart> sessionProductInCart = Product.GetProductsInCartByUIntList((List<uint>)DataProvider.ByteArrayToObject(HttpContext.Session.Get("cart")));
            ViewData.Add("sessionProductInCart", sessionProductInCart);
            return View(sessionUSer);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
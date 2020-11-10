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
    public class ProductController : Controller
    {
        public IActionResult ShoppingCart()
        {
            Account sessionUSer = (Account) DataProvider.ByteArrayToObject(HttpContext.Session.Get("SessionUser"));
            List<ProductInCart> sessionProductInCart = Product.GetProductsInCartByUIntList((List<uint>)DataProvider.ByteArrayToObject(HttpContext.Session.Get("cart")));
            ViewData.Add("sessionProductInCart", sessionProductInCart);
            return View(sessionUSer);
        }
        [HttpPost]
        public ActionResult Shop(uint userID)
        {
            Account sessionUSer = (Account) DataProvider.ByteArrayToObject(HttpContext.Session.Get("SessionUser"));
            List<ProductInCart> sessionProductInCart = Product.GetProductsInCartByUIntList((List<uint>)DataProvider.ByteArrayToObject(HttpContext.Session.Get("cart")));
            ViewData.Add("sessionProductInCart", sessionProductInCart);
            ViewData.Add("ProductList", Product.GetFavorateProductsByUserID(userID, 0));
            return View(sessionUSer);
        }
        public IActionResult Shop(string q)
        {
            Account sessionUSer = (Account) DataProvider.ByteArrayToObject(HttpContext.Session.Get("SessionUser"));
            ViewData.Add("ProductList", Product.SearchProduct(q,0));
            List<ProductInCart> sessionProductInCart = Product.GetProductsInCartByUIntList((List<uint>)DataProvider.ByteArrayToObject(HttpContext.Session.Get("cart")));
            ViewData.Add("sessionProductInCart", sessionProductInCart);
            return View(sessionUSer);
        }
        public IActionResult ProductPage(uint id)
        {
            ProductInPage product = ProductInPage.GetProductInPageByID(id);
            if (product == null)
            {
                return View("Error");
            }
            ViewData.Add("ProductInfo",product);
            Account sessionUSer = (Account) DataProvider.ByteArrayToObject(HttpContext.Session.Get("SessionUser"));
            List<ProductInCart> sessionProductInCart = Product.GetProductsInCartByUIntList((List<uint>)DataProvider.ByteArrayToObject(HttpContext.Session.Get("cart")));
            ViewData.Add("sessionProductInCart", sessionProductInCart);
            return View(sessionUSer);
        }
        public JsonResult GetProductByPageIndex(int index)
        {
            return Json(Product.GetProductsByPageIndex(index));
        }
        public JsonResult AddToCart(uint id)
        {
            List<uint> productsInCart = (List<uint>)DataProvider.ByteArrayToObject(HttpContext.Session.Get("cart"));
            if (productsInCart == null)
            {
                productsInCart = new List<uint>();
            }
            productsInCart.Add(id);
            HttpContext.Session.Set("cart", DataProvider.ObjectToByteArray(productsInCart));
            return Json(true);
        }
        public JsonResult RemoveProductInCart(uint id)
        {
            List<uint> productsInCart = (List<uint>)DataProvider.ByteArrayToObject(HttpContext.Session.Get("cart"));
            if (productsInCart == null)
            {
                return Json(false);
            }
            productsInCart.Remove(id);
            HttpContext.Session.Set("cart", DataProvider.ObjectToByteArray(productsInCart));
            return Json(true);
        }
    }
}
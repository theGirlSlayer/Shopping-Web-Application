using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Estore.Models;
using Microsoft.AspNetCore.Http;

namespace Estore.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(string txtFirstName, string txtLastName, string txtDisplayName, string txtPhoneNumber, string txtEmail, string txtAddress, string txtUsername, string txtPassword, string txtConfirmPassword)
        {
            Account CurrentUser = Account.Register(txtFirstName,txtLastName,txtDisplayName, txtPhoneNumber,txtEmail,txtAddress,txtUsername,txtPassword);
            HttpContext.Session.Set("SessionUser", DataProvider.ObjectToByteArray(CurrentUser));
            List<ProductInCart> sessionProductInCart = Product.GetProductsInCartByUIntList((List<uint>)DataProvider.ByteArrayToObject(HttpContext.Session.Get("cart")));
            ViewData.Add("sessionProductInCart", sessionProductInCart);
            return View("Index", CurrentUser);
        }
        public IActionResult Login()
        {
            List<ProductInCart> sessionProductInCart = Product.GetProductsInCartByUIntList((List<uint>)DataProvider.ByteArrayToObject(HttpContext.Session.Get("cart")));
            ViewData.Add("sessionProductInCart", sessionProductInCart);
            return View();
        }
        [HttpPost]
        public IActionResult Index(string txtUsername, string txtPassword)
        {
            Account CurrentUser = Models.Account.Login(txtUsername,txtPassword);
            List<ProductInCart> sessionProductInCart = Product.GetProductsInCartByUIntList((List<uint>)DataProvider.ByteArrayToObject(HttpContext.Session.Get("cart")));
            ViewData.Add("sessionProductInCart", sessionProductInCart);
            if ( CurrentUser != null)
            {
                HttpContext.Session.Set("SessionUser",DataProvider.ObjectToByteArray(CurrentUser));
                return View(CurrentUser);
            }
            else
            {
                ViewData.Add("Message","Invalid username or password");
                return View("Login");
            }
        }
        public IActionResult UserSetting()
        {
            AccountInfo account = (AccountInfo)DataProvider.ByteArrayToObject(HttpContext.Session.Get("SessionUser"));
            List<ProductInCart> sessionProductInCart = Product.GetProductsInCartByUIntList((List<uint>)DataProvider.ByteArrayToObject(HttpContext.Session.Get("cart")));
            ViewData.Add("sessionProductInCart", sessionProductInCart);
            if (account == null)
            {
                return View("Login");
            }
            return View(account);
        }
        [HttpPost]
        public IActionResult UserSetting(string txtFirstName, string txtLastName, string txtDisplayName, string txtPhoneNumber, string txtEmail, string txtAddress, string txtUsername, string txtPassword, string txtConfirmPassword, string txtFacebook, string txtTwiteer, string txtInstagram)
        {
            Account account = Account.Update(txtFirstName, txtLastName,txtDisplayName,txtPhoneNumber, txtEmail, txtFacebook, txtTwiteer, txtInstagram, txtAddress, txtUsername, txtPassword);
            List<ProductInCart> sessionProductInCart = Product.GetProductsInCartByUIntList((List<uint>)DataProvider.ByteArrayToObject(HttpContext.Session.Get("cart")));
            ViewData.Add("sessionProductInCart", sessionProductInCart);
            if (account == null)
            {
                ViewData.Add("Message", "Password is incorrect, please check your password again");
                return View("UserSetting", (Account)DataProvider.ByteArrayToObject(HttpContext.Session.Get("SessionUser")));
            }
            else
            {
                HttpContext.Session.Set("SessionUser", DataProvider.ObjectToByteArray(account));
                return View("Index", account);
            }
        }
    }
}
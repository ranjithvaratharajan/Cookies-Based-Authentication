using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ViewModel;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        const string URLPREFIX = "api/values";
        public async Task<IActionResult> Login()
        {
            var loginFlag = false;
            LoginModel login = new LoginModel();
            login.Username = "123";
            login.Password = "123";

            HttpResponseMessage response = await ServiceCall<LoginModel>.postData(URLPREFIX + "/authenticate", login);
            if (response.IsSuccessStatusCode)
            {
                loginFlag = true;
            }

            if (loginFlag)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        
        public async Task<IActionResult> Index()
        {
            var data = await ServiceCall<LoginModel>.getData("/api/values/get");
            if (data.IsSuccessStatusCode)
            {
                ViewBag.data = await data.Content.ReadAsAsync<List<LoginModel>>();
            }
            return View();
        }
        
    }
}

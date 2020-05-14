using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebCrawler.Core.Helpers;
using WebCrawler.Filters;
using WebCrawler.Models;

namespace WebCrawler.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AppAuthorize]
        public IActionResult Index()
        {

            //Creating tables




            return View();
        }

        [AppAuthorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult GenerateSaltAndHash(string plainPassword)
        {
            string salt = PasswordHelper.GenerateSalt(32);
            string password = PasswordHelper.HashPassword(salt, plainPassword);
            return Json(new { salt = salt, password = password });
        }
    }
}

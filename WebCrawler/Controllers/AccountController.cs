using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCrawler.Core.Models;
using WebCrawler.Service.Interfaces;
using WebCrawler.AppHelpers;
using WebCrawler.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using WebCrawler.Core.Helpers;

namespace WebCrawler.Controllers
{
    public class AccountController : Controller
    {

        private readonly DBContext _context;
        private readonly IAccountService _accountService;

        public AccountController(DBContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>  Login(string UserName, string Password)
        {
            var user = await _accountService.Login(UserName, Password);
            var getWebsite = await _context.Websites.FirstOrDefaultAsync();
            if (user.StatusCode == StatusCodes.Status200OK)
            {
                SessionModel model = new SessionModel();



                model.User = (Users)user.Result;

                if (getWebsite != null)
                {
                    model.Website = getWebsite;
                }

                HttpContext.Session.SetSession(model);
              

                return RedirectToAction("Index", "Scrapper");
            }

            else
            {
                ViewBag.ErrorMessage = "Invalid Username or Password";
                return View();
            }


        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Users user)
        {

            var existing = _context.Users.FirstOrDefault(x => x.UserName == user.UserName);

            if (existing != null)
            {
                ViewBag.Message = "Error: Username alerady Exist";
            }
            else
            {
                user.IsActive = 1;
                user.IsVerified = 1;
                user.UserRoleId = 2;

                user.Salt = PasswordHelper.GenerateSalt(32);
                user.Password = PasswordHelper.HashPassword(user.Salt, user.Password);

                _context.Users.Add(user);
                _context.SaveChanges();

                ViewBag.Message = "User Created Successfully";
            }


            return View();
        }



        public IActionResult Logout()
        {
            HttpContext.Session.KillSession();
            return RedirectToAction("Login", "Account");
        }


    }
}
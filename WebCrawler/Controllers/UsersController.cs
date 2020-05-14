using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCrawler.Core.Models;
using WebCrawler.Filters;
using Microsoft.EntityFrameworkCore;
using WebCrawler.Core.Helpers;

namespace WebCrawler.Controllers
{
    public class UsersController : Controller
    {
        private readonly DBContext _dbContext;

        public UsersController(DBContext context)
        {
            _dbContext = context;
        }

        [AppAuthorize]
        public IActionResult Index(string name="", string email="")
        {
            List<Users> GetAllUsers = new List<Users>();

            name = name.ToLower().Trim();
            email = email.ToLower().Trim();
            if (!String.IsNullOrEmpty(name))
            {
                GetAllUsers = _dbContext.Users.Where(x=> (x.FirstName +" "+ x.LastName).ToLower().Trim().Contains(name) || (x.FirstName +" "+ x.LastName).ToLower().Trim() == name).ToList();

            }
            else if(!String.IsNullOrEmpty(email))
            {
                GetAllUsers = _dbContext.Users.Where(x=>x.Email.ToLower().Trim().Contains(email) || x.Email.ToLower().Trim()==email).ToList();

            }
            else
            {
                GetAllUsers = _dbContext.Users.ToList();

            }

            return View(GetAllUsers);
        }
        

        public IActionResult AddUpdateUser(int userId=0)
        {
            if (userId > 0)
            {
            var getuser = _dbContext.Users.FirstOrDefault(x => x.UserId == userId);
                return View(getuser);

            }
            else
            {
                return View();
            }

        }
       
        [HttpPost]
        public IActionResult AddUpdateUser(Users user)
        {
            try
            {
                var conflict = _dbContext.Users.FirstOrDefault(x => x.UserName.ToLower().Trim() == user.UserName.ToLower().Trim() && x.UserId!=user.UserId);

                if (conflict == null)
                {
                 
                    if (user.UserId > 0)
                    {
                        var existingUser = _dbContext.Users.FirstOrDefault(x => x.UserId == user.UserId);
                        existingUser.FirstName = user.FirstName;
                        existingUser.LastName = user.LastName;
                        existingUser.Email = user.Email;
                        existingUser.UserName = user.UserName;

                        if (!String.IsNullOrEmpty(user.Password))
                        {
                            existingUser.Salt = PasswordHelper.GenerateSalt(32);
                            existingUser.Password = PasswordHelper.HashPassword(existingUser.Salt, user.Password);
                        }

                        existingUser.IsActive = user.IsActive;
                        existingUser.IsVerified = user.IsVerified;
                        existingUser.UserRoleId = user.UserRoleId;

                        _dbContext.SaveChanges();

                        ViewBag.Message = "User Updated Successfuly";
                    }
                
                    else
                    {
                        if(!String.IsNullOrEmpty(user.Password)){
                            string plainText = user.Password;

                            user.Salt = PasswordHelper.GenerateSalt(32);
                            user.Password = PasswordHelper.HashPassword(user.Salt, plainText);

                        }


                        user.UserRoleId = 2;

                        _dbContext.Users.Add(user);
                        _dbContext.SaveChanges();
                        ViewBag.Message = "User Added Successfuly";
                    }
                }

                else
                {
                    ViewBag.Message = "User with same username already exist";
                }
            }

            catch(Exception e)
            {
                ViewBag.Message = "An Error Occured";
            }



            return View();

        }





        public IActionResult DeleteUser(int userId)
        {
            try
            {

                if (userId == 1)
                {

                }
                else
                {
                    var findUser = _dbContext.Users.FirstOrDefault(x => x.UserId == userId);
                    if (findUser != null)
                    {
                        _dbContext.Users.Remove(findUser);
                        _dbContext.SaveChanges();

                    }
                }
              
            }
            catch(Exception e)
            {

            }

            return RedirectToAction("Index", "Users");

        }
    }
}
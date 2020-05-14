using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCrawler.Core.Dtos.Configuration;
using WebCrawler.Core.Models;
using WebCrawler.Filters;
using Microsoft.EntityFrameworkCore;
using WebCrawler.AppHelpers;

namespace WebCrawler.Controllers
{
    public class ConfigurationController : Controller
    {

        private readonly DBContext _context;

        public ConfigurationController(DBContext context)
        {
            _context = context;
        }


        [AppAuthorize]
        public IActionResult Index()
        {

            var getExisting = _context.Websites.FirstOrDefault();



           
            ViewBag.MessageType= TempData["MessageType"] !=null? TempData["MessageType"]:0;
            ViewBag.Message= TempData["Message"]!=null ? TempData["Message"]:"";
            return View(getExisting);
        }
        
        [HttpPost]
        public IActionResult SaveConfiguration(SaveConfigurationRequest request)
        {

            if (String.IsNullOrEmpty(request.WebsiteName))
            {
                request.WebsiteName = "";
            }
            if (String.IsNullOrEmpty(request.WebsiteUrl))
            {
                request.WebsiteUrl = "";
            }

            try
            {

                if (request.WebsiteId > 0)
                {
                    var getExisting = _context.Websites.Where(x => x.Id == request.WebsiteId).FirstOrDefault();
                    if (getExisting != null)
                    {
                        getExisting.WebsiteName = request.WebsiteName;
                        getExisting.WebsiteUrl = request.WebsiteUrl;
                        _context.SaveChanges();

                        var getsession = HttpContext.Session.GetSession();
                        getsession.Website = getExisting;
                        HttpContext.Session.SetSession(getsession);


                    }

                    else
                    {
                        Websites website = new Websites()
                        {
                            WebsiteName = request.WebsiteName,
                            WebsiteUrl = request.WebsiteUrl
                        };

                        _context.Websites.Add(website);
                        _context.SaveChanges();

                        var getsession = HttpContext.Session.GetSession();
                        getsession.Website = website;
                        HttpContext.Session.SetSession(getsession);
                    }
                }

              

                else
                {
                    Websites website = new Websites()
                    {
                        WebsiteName = request.WebsiteName,
                        WebsiteUrl = request.WebsiteUrl
                    };

                    _context.Websites.Add(website);
                    _context.SaveChanges();


                    var getsession = HttpContext.Session.GetSession();
                    getsession.Website = website;
                    HttpContext.Session.SetSession(getsession);
                }





                TempData["MessageType"] = 200;
                TempData["Message"] = "Website Saved Successfully";
                return RedirectToAction("Index", "Configuration");

            }
            catch(Exception e)
            {
                TempData["MessageType"] = 500;
                TempData["Message"] = "An Error Occured. Please Try Again";
                return RedirectToAction("Index", "Configuration");
            }


        }

    }
}
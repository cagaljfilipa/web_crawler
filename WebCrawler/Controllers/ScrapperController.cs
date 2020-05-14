using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCrawler.Core.Models;
using WebCrawler.Core.Scrapper;
using WebCrawler.Filters;
using Microsoft.EntityFrameworkCore;
using WebCrawler.AppHelpers;
using HtmlAgilityPack;
using WebCrawler.Core.StaticData;
using System.Text.RegularExpressions;
using WebCrawler.Core.Settings;
using WebCrawler.Core.Helpers;
using WebCrawler.Core.ViewModels;
using WebCrawler.Models;

namespace WebCrawler.Controllers
{
    public class ScrapperController : Controller
    {

        private readonly DBContext _context;

        public ScrapperController(DBContext context)
        {
            _context = context;
        }


        
        public IActionResult Index()
        {
            var getExisting = _context.Websites.FirstOrDefault();
            return View(getExisting);
        }


        public IActionResult Start([FromBody]ScrapperRequest request)
        {
            const string domain = "https://e-brojevi.udd.hr";

            List<ScrapperResponse> responseList = new List<ScrapperResponse>();

            var GetWebsite = HttpContext.Session.GetSession().Website;

            if (request.StartScrapper)
            {

                HtmlWeb web = new HtmlWeb();

                HtmlDocument document = web.Load(GetWebsite.WebsiteUrl);


                List<ChildItems> childList = new List<ChildItems>();
                foreach (HtmlNode link in document.DocumentNode.SelectNodes("//a[@href]"))
                {

                   

                    string hrefValue = link.GetAttributeValue("href", string.Empty);
                    if (!hrefValue.Contains("file") && !hrefValue.Contains("_") && !hrefValue.Contains("-"))
                    {

                        string urlCode = Regex.Replace(hrefValue, @"[^\d]", "");
                        int urlno = Convert.ToInt32(urlCode);

                        if (urlno >= 100 && urlno <= 180)
                        {
                            var child= new ChildItems();
                            child.ChildUrl = domain + "/" + urlno+".htm";
                            child.ParentCategory = CategoriesNames.Category1;
                            childList.Add(child);
                        }
                        if (urlno >= 200 && urlno <= 297)
                        {
                            var child = new ChildItems();
                            child.ChildUrl = domain + "/" + urlno + ".htm";
                            child.ParentCategory = CategoriesNames.Category2;
                            childList.Add(child);
                        }


                        if (urlno >= 300 && urlno <= 385)
                        {
                            var child = new ChildItems();
                            child.ChildUrl = domain + "/" + urlno + ".htm";

                            child.ParentCategory = CategoriesNames.Category3;

                            childList.Add(child);
                        }

                        if (urlno >= 400 && urlno <= 495)
                        {
                            var child = new ChildItems();
                            child.ChildUrl = domain + "/" + urlno + ".htm";

                            child.ParentCategory = CategoriesNames.Category4;


                            childList.Add(child);
                        }

                        if (urlno >= 500 && urlno <= 586)
                        {
                            var child = new ChildItems();
                            child.ChildUrl = domain + "/" + urlno + ".htm";

                            child.ParentCategory = CategoriesNames.Category5;


                            childList.Add(child);
                        }

                        if (urlno >= 620 && urlno <= 650)
                        {
                            var child = new ChildItems();
                            child.ChildUrl = domain + "/" + urlno + ".htm";

                            child.ParentCategory = CategoriesNames.Category6;


                            childList.Add(child);
                        }

                        if (urlno >= 900 && urlno <= 1520)
                        {
                            var child = new ChildItems();
                            child.ChildUrl = domain + "/" + urlno + ".htm";

                            child.ParentCategory = CategoriesNames.Category7;

                            childList.Add(child);
                        }


                    }

                }



                var list = childList.GroupBy(x => x.ParentCategory);

                List<MainObject> objList = new List<MainObject>();

                foreach (var item in childList.GroupBy(x => x.ParentCategory))
                {
                    MainObject obj = new MainObject();
                    obj.CategoryName = item.Key;
                    obj.Child = item.Select(x => new ChildObject { ChildUrl=x.ChildUrl}).ToList();
                    objList.Add(obj);
                }

                var getsession = HttpContext.Session.GetSession();
                getsession.MainObjects = objList;
                HttpContext.Session.SetSession(getsession);


                return Json(new { status = 200, result = objList });



            }
            else
            {

                HtmlWeb web = new HtmlWeb();

                HtmlDocument document = web.Load(GetWebsite.WebsiteUrl);

                var table = document.DocumentNode.SelectNodes("/html/body/table/tr");

                var rows = document.DocumentNode.SelectNodes("//tr").Select((z, i) => new
                {
                    RowNumber = i,
                    Cells = z.ChildNodes.Where(c => c.NodeType == HtmlNodeType.Element)
                }).ToList();

                foreach (var col in rows)
                {
                    try
                    {
                        var element = col.Cells.Skip(1).First();
                        foreach (HtmlNode data in element.SelectNodes("//*[@color]"))
                        {
                            if (data.GetAttributeValue("color", "#666666") == "#666666")
                            {

                                var here = data.InnerText;

                            }
                            var alink = data;
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }

            }


            return Json(new { status = 500 });
        }


        public IActionResult GetDataByCategory(string categoryName)
        {
           
            List<DataViewModel> dataList = new List<DataViewModel>();


            var mainObj = HttpContext.Session.GetSession().MainObjects;
            var listChildItems = mainObj.Where(x => x.CategoryName.ToLower() == categoryName.ToLower()).FirstOrDefault();


            var categoryModel = new Categories();
            categoryModel.CategoryName = listChildItems.CategoryName;
            try
            {


                foreach (var child in listChildItems.Child)
                {

                    var model = new DataViewModel();


                    HtmlWeb web = new HtmlWeb();
                    HtmlDocument document = web.Load(child.ChildUrl);

                    //try for image
                    var itemsModel = new Items();
                    try
                    {
                        foreach (HtmlNode link in document.DocumentNode.SelectNodes("//img"))
                        {
                            var src = link.GetAttributeValue("src", "");
                            if (src.Contains("e_strukture"))
                            {
                                string completesource = AppSettings.Domain + src;
                                itemsModel.ItemImage = completesource;
                            }
                        }

                    }
                    catch (Exception e)
                    {

                    }



                    //try for rest of the data
                    try
                    {
                        foreach (HtmlNode data in document.DocumentNode.SelectNodes("//*[@size]"))
                        {
                            if (data.GetAttributeValue("size", "3") == "3")
                            {

                                var fontcolor = data.GetAttributeValue("color", "");
                                var findColor = ScrapperHelper.FindColor(fontcolor);

                                var cleanInnerText = ScrapperHelper.CleanInnerText(data.InnerText);
                                var split = cleanInnerText.Split(" ", 2);

                                var itemcode = split[0];
                                var itename = split[1];

                                itemsModel.ItemCode = itemcode;
                                itemsModel.ItemName = itename;
                                itemsModel.ItemColor = findColor;
                            }

                            if (data.GetAttributeValue("size", "2") == "2")
                            {
                                var cleanDescription = ScrapperHelper.CleanInnerText(data.InnerText);
                                itemsModel.ItemDesc = itemsModel.ItemDesc + cleanDescription;
                            }


                        }
                    }



                    catch (Exception e)
                    {

                    }

                    model.Category = categoryModel;
                    model.Items = itemsModel;

                    SaveData(model);
                    dataList.Add(model);


                    //Lets save the data







                }


                return Json(new { status = 200 });

            }
            catch(Exception e)
            {
                return Json(new { status = 200 });
            }
        }


        public void SaveData(DataViewModel model)
        {

            var sessionuser = HttpContext.Session.GetSession().User;

            try
            {
                //find category first

                var category = _context.Categories.FirstOrDefault(x => x.CategoryName.Contains(model.Category.CategoryName));

                if (category != null)
                {

                    model.Items.CategoryId = category.CategoryId;
                    model.Category.CategoryId = category.CategoryId;
                    _context.SaveChanges();
                }
                else
                {
                    _context.Categories.Add(model.Category);
                    _context.SaveChanges();
                    model.Category.CategoryId = category.CategoryId;
                    model.Items.CategoryId = model.Category.CategoryId;


                }
                var getItem = _context.Items.FirstOrDefault(x => x.ItemCode.Trim() == model.Items.ItemCode.Trim() && x.CategoryId== model.Category.CategoryId && x.UserId == sessionuser.UserId);

                    if (getItem == null)
                    {
                        
                        model.Items.UserId = sessionuser.UserId;
                        model.Items.CreatedTime = DateTime.Now;

                        _context.Items.Add(model.Items);
                    _context.SaveChanges();
                    }
                   

                  
                
             
            }
            catch(Exception e)
            {
                var here = e.Message;
            }
        }


        public IActionResult ViewCategoriesData(string categoryName = "")
        {
            ItemsDataResponse response = new ItemsDataResponse();

            response.CategoryName = "";

            if (String.IsNullOrEmpty(categoryName))
            {
               
                return View(response);
            }

            response.CategoryName = categoryName;


            var sessionuser = HttpContext.Session.GetSession().User;


            var category = _context.Categories.FirstOrDefault(x =>  x.CategoryName.ToLower().Trim().Contains(categoryName.ToLower().Trim()) ||  x.CategoryName.ToLower().Trim() == categoryName.ToLower().Trim());

            if (category != null)
            {
                response.CategoryName = category.CategoryName;
                var getItems = _context.Items.Where(x => x.CategoryId == category.CategoryId && (x.UserId == sessionuser.UserId || x.UserId==1)).ToList();
            
                if(getItems!=null && getItems.Count > 0)
                {
                    response.ItemsList = getItems;
                    return View(response);
                }
                else
                {
                    return View(response);
                }
            
            
            }

            return View(response);
           
        }
    
    
        public IActionResult GetSingleItem(int itemId = 0, string itemCode="", string itemName="")
        {
            SingleItemResponse response = new SingleItemResponse();

            var sessionuser = HttpContext.Session.GetSession().User;

            Items getItem = new Items();
            if (itemId > 0)
            {
                getItem = _context.Items.FirstOrDefault(x => x.ItemId == itemId && (x.UserId==sessionuser.UserId || x.UserId==1));
            }

            else if (!String.IsNullOrEmpty(itemCode))
            {
               getItem = _context.Items.FirstOrDefault(x => x.ItemCode.ToLower().Trim().Contains(itemCode.ToLower().Trim()) || x.ItemCode.ToLower().Trim() == itemCode.ToLower().Trim() && (x.UserId == sessionuser.UserId || x.UserId == 1));
            }

            else if (!String.IsNullOrEmpty(itemName))
            {
                getItem = _context.Items.FirstOrDefault(x => x.ItemName.ToLower().Trim().Contains(itemName.ToLower().Trim()) || x.ItemName.ToLower().Trim()== itemName.ToLower().Trim() && (x.UserId == sessionuser.UserId || x.UserId == 1));
            }


          

            if (getItem != null)
            {

                var getcategory = _context.Categories.FirstOrDefault(x => x.CategoryId == getItem.CategoryId);
                var getuser = _context.Users.FirstOrDefault(x => x.UserId == getItem.UserId);

                if (getcategory != null)
                {
                    response.Category = getcategory;
                }
                if (getuser != null)
                {
                    response.User = getuser;
                }


                response.Item = getItem;

                return View(response);


            }



            return View(response);
        }
    
        [AppAuthorize]
        public IActionResult AddUpdateItem(int itemId=0)
        {
            var currentUser = HttpContext.Session.GetSession().User;
            var getcategoreis = _context.Categories.ToList();
            ViewData["Categories"] = getcategoreis;
            if (itemId > 0)
            {
                var getItem = _context.Items.FirstOrDefault(x => x.ItemId == itemId);
                if (getItem != null)
                {
                  
                    return View(getItem);
                }
                else
                {
                    return View();
                }
            }

            return View();
        }

        [AppAuthorize]
        [HttpPost]
        public IActionResult AddUpdateItem(Items item)
        {

            try
            {

                var currentUser = HttpContext.Session.GetSession().User;

                if (item.ItemId > 0)
                {
                    var existing = _context.Items.FirstOrDefault(x => x.ItemId == item.ItemId && x.UserId==currentUser.UserId);

                    if (existing != null)
                    {
                        existing.ItemImage = item.ItemName;
                        existing.ItemCode = item.ItemCode;
                        existing.ItemDesc = item.ItemDesc;
                        existing.ItemColor = item.ItemColor;
                        existing.CategoryId= item.CategoryId;
                        existing.UserId = currentUser.UserId;
                        existing.CreatedTime = DateTime.Now;
                        _context.SaveChanges();
                    }
                    else
                    {
                        item.CreatedTime = DateTime.Now;
                        item.UserId = currentUser.UserId;
                        _context.Items.Add(item);
                        _context.SaveChanges();
                    }
                }

                else
                {
                    item.CreatedTime = DateTime.Now;
                    item.UserId = currentUser.UserId;
                    _context.Items.Add(item);
                    _context.SaveChanges();
                }
            }
            catch(Exception e)
            {

            }
            return View();
        }

        [AppAuthorize]
        public IActionResult DeleteItem(int itemId = 0)
        {
            try
            {
                var currentUser = HttpContext.Session.GetSession().User;

                var exit = _context.Items.FirstOrDefault(x => x.ItemId == itemId && x.UserId == currentUser.UserId);

                if (exit != null)
                {
                    _context.Items.Remove(exit);
                    _context.SaveChanges();
                }
            }
            catch(Exception e)
            {

            }

            return RedirectToAction("GetSingleItem", "Scrapper");

        }
        

    }
}
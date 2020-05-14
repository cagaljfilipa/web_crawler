﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCrawler.Filters;

namespace WebCrawler.Controllers
{
    public class DataController : Controller
    {

        [AppAuthorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
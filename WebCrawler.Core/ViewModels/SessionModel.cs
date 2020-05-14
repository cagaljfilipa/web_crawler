using System;
using System.Collections.Generic;
using System.Text;
using WebCrawler.Core.Models;
using WebCrawler.Core.Scrapper;

namespace WebCrawler.Core.ViewModels
{
    public class SessionModel
    {
       public Users User { get; set; }
       public Websites Website { get; set; }

       public List<MainObject> MainObjects { get; set; }

    }
}

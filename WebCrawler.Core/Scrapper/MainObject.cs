using System;
using System.Collections.Generic;
using System.Text;

namespace WebCrawler.Core.Scrapper
{
    public class MainObject
    {
        public string CategoryName { get; set; }
        public List<ChildObject> Child { get; set; }
    }

    public class ChildObject
    {
        public string ChildUrl { get; set; }
    }
}

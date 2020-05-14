using System;
using System.Collections.Generic;
using System.Text;

namespace WebCrawler.Core.Scrapper
{
    public class ScrapperResponse
    {
        public string CategoryName { get; set; }
        public List<ChildItems> ChildList { get; set; }
    }

    public class ChildItems
    {
        public string ParentCategory { get; set; }
        public string ChildUrl { get; set; }
    }

}

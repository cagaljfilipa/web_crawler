using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Core.Models;

namespace WebCrawler.Models
{
    public class ItemsDataResponse
    {
        public string CategoryName { get; set; }
        public List<Items> ItemsList { get; set; }
    }
}

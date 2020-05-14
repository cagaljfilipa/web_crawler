using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCrawler.Core.Models;

namespace WebCrawler.Models
{
    public class SingleItemResponse
    {
        public Users User { get; set; }
        public Items Item { get; set; }
        public Categories Category { get; set; }
    }
}

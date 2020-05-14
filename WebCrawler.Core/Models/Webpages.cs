using System;
using System.Collections.Generic;

namespace WebCrawler.Core.Models
{
    public partial class Webpages
    {
        public int Id { get; set; }
        public int? WebsiteId { get; set; }
        public string PageName { get; set; }
        public string PageUrl { get; set; }
    }
}

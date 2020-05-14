using System;
using System.Collections.Generic;

namespace WebCrawler.Core.Models
{
    public partial class Websites
    {
        public int Id { get; set; }
        public string WebsiteName { get; set; }
        public string WebsiteUrl { get; set; }
    }
}

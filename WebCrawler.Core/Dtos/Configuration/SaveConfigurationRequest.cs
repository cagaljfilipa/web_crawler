using System;
using System.Collections.Generic;
using System.Text;

namespace WebCrawler.Core.Dtos.Configuration
{
    public class SaveConfigurationRequest
    {
        public int WebsiteId { get; set; }
        public string WebsiteName { get; set; }
        public string WebsiteUrl { get; set; }
    }
}

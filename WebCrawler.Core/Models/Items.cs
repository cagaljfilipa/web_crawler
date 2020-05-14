using System;
using System.Collections.Generic;

namespace WebCrawler.Core.Models
{
    public partial class Items
    {
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemDesc { get; set; }
        public string ItemImage { get; set; }
        public int? CategoryId { get; set; }
        public string ItemColor { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}

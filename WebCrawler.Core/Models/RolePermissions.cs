using System;
using System.Collections.Generic;

namespace WebCrawler.Core.Models
{
    public partial class RolePermissions
    {
        public int Id { get; set; }
        public string PermissionName { get; set; }
        public int? RoleId { get; set; }
    }
}

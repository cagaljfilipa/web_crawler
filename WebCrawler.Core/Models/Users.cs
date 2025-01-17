﻿using System;
using System.Collections.Generic;

namespace WebCrawler.Core.Models
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public ulong? IsVerified { get; set; }
        public ulong? IsActive { get; set; }
        public int? UserRoleId { get; set; }
    }
}

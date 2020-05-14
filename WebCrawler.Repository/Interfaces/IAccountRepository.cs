using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Core.Models;

namespace WebCrawler.Repository.Interfaces
{
    public interface IAccountRepository
    {
        public Task<Users> Login(string username, string password);

    }
}

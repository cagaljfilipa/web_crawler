using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Core.Dtos;

namespace WebCrawler.Service.Interfaces
{
    public interface IAccountService
    {
        Task<DomainResult> Login(string username, string password);
    }
}

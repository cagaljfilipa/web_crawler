using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCrawler.Core.Dtos;
using WebCrawler.Repository.Interfaces;
using WebCrawler.Service.Interfaces;

namespace WebCrawler.Service.Implementation
{
    public class AccountService: IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public  async Task<DomainResult> Login(string username, string password)
            { 

            var result = new DomainResult()
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };


            var loginuser =await  _accountRepository.Login(username, password);

            if (loginuser == null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                return result;
                
            }

            result.StatusCode = StatusCodes.Status200OK;
            result.Result = loginuser;

            return result;

        }
    }
}

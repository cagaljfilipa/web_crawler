using System;
using System.Collections.Generic;
using System.Text;
using WebCrawler.Core.Models;
using WebCrawler.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebCrawler.Core.Helpers;
using System.Threading.Tasks;

namespace WebCrawler.Repository.Implementation
{
    public class AccountRepository : IAccountRepository
    {
        public readonly DBContext _context;

        public AccountRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Users> Login(string username, string password)
        {
            try
            {
                var getUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
                if (getUser != null)
                {
                    bool verifyHash = PasswordHelper.ValidatePassword(getUser.Salt, getUser.Password, password);

                    if (verifyHash)
                    {
                        return getUser;
                    }
                    else
                    {
                        return null;
                    }
                
                }

                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}

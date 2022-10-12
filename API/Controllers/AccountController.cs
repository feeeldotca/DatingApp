using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly DataContext _Context;
        
        public AccountController(DataContext Context)    {
            _Context = Context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(string username, string password){
            using var hmac = new HMACSHA512();
            var user = new AppUser{
                UserName =username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key
            };

            _Context.Users.Add(user);
            await _Context.SaveChangesAsync();
            return user;
        }
    }
}
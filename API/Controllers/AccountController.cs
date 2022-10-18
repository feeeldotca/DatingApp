using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        
        public AccountController(DataContext context)    {
            _context = context;
        }

        [HttpPost("register")]
        
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto){
            if(await UserExists(registerDto.Username)) return BadRequest("Username is taken");

            using var hmac = new HMACSHA512();
            var user = new AppUser{
                UserName = registerDto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUser>> Login(LoginDto loginDto, int i)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x=>x.UserName==loginDto.Username);
            if(user == null) return Unauthorized("Invalid username");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for(int j = 0; j<computeHash.Length; j++){
                if(computeHash[j] != user.PasswordHash[j])
                return Unauthorized("Invalid Password");
            }
                
            return user;
        }

        private async Task<bool> UserExists(string username){
            return await _context.Users.AnyAsync(u=>u.UserName == username.ToLower());
        }
    }
}
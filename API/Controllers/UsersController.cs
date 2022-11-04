using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{

    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;   
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users =_userRepository.GetUsersAsync();
            return Ok(await users);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<AppUser>> GetUserById(int id)
        {
            var user = _userRepository.GetUserByIdAsync(id);
            return await user;
        }

        [HttpGet("{username}")]

        public async Task<ActionResult<AppUser>> GetUserById(string username)
        {
            var user = _userRepository.GetUserByUsernameAsync(username);
            return await user;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{

    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        
        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
            _photoService = photoService;
            _mapper = mapper;
            _userRepository = userRepository;               
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetMembersAsync();
            return Ok(users);
        }
/*
        [HttpGet("{id}")]

        public async Task<ActionResult<MemberDto>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return _mapper.Map<MemberDto>(user);
        }*/

        [HttpGet("{username}")]

        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _userRepository.GetMemberAsync(username);
            //return _mapper.Map<MemberDto>(user);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto){
            var username = User.GetUsername();
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if(user == null) return NotFound();
            _mapper.Map(memberUpdateDto, user);
            if(await _userRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to update user");
        }
         [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file){
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            if(user == null) return NotFound();
            var result = await _photoService.AddPhotoAsync(file);

            if(result.Error != null) return BadRequest();
            var photo = new Photo {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if(user.Photos.Count == 0) photo.IsMain = true;

            user.Photos.Add(photo);
            if(await _userRepository.SaveAllAsync()) 
            {
                return CreatedAtAction(nameof(GetUsers),                 
                new {username = user.UserName}, 
                _mapper.Map<PhotoDto>(photo)); //_mapper.Map<PhotoDto>(photo);
            }

            return BadRequest("Problem adding photo");
        }   

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId){
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            if(user == null) return NotFound();

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);
            if(photo.IsMain) return BadRequest("this is already your main photo");

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);

            photo.IsMain = true;

            if( await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Problem setting the main photo");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId) {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            var photo = user.Photos.FirstOrDefault(x=>x.Id == photoId);

            if(photo == null) return NotFound();

            if(photo.IsMain) return BadRequest("you can not delete your main photo!");

            if(photo.PublicId != null) {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if(result.Error != null) return BadRequest(result.Error.Message);
            }

            user.Photos.Remove(photo);

            if(await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Problem deleting photo");
        }
    }
}


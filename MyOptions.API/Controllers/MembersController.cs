using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyOptions.API.Data;
using MyOptions.API.Dtos;
using MyOptions.API.Models;

namespace MyOptions.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberRepository _repo;
        private readonly IMapper _mapper;        
        public MembersController(IMemberRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;        
        }


        [HttpGet("getmembers", Name = "GetMembers")]
        public async Task<IActionResult> GetMembers()
        {
            var currentUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var users = await _repo.GetMembers(currentUser);
            if (users == null)
            {
                return NotFound();
            }

            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            return Ok(usersToReturn);
        }   

        [HttpGet("getmember/{id}", Name = "GetMember")]
        public async Task<IActionResult> GetMember(int? userid)
        {

            if (userid == null)
            {
                return NotFound();
            }

            var user = await _repo.GetMember(userid.Value);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }  


        [HttpGet("getmembertrades/{userid}", Name = "GetMemberTrades")]
        public async Task<IActionResult> GetMemberTrades(int? userid)
        {

            if (userid == null)
            {
                return NotFound();
            }

            var user = await _repo.GetMemberTrades(userid.Value);
            if (user == null)
            {
                return NotFound();
            }

            var userToReturn = _mapper.Map<UserForListDto>(user);

            return Ok(userToReturn);
        }         
    }
}

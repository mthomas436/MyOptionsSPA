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
    public class TestController : ControllerBase
    {
        private readonly ITestRepository _repo;
        public int currentUserId;        
        public TestController(ITestRepository repo)
        {
            _repo = repo;   
        }

        [HttpGet("getusertrades/{userid}",Name="GetUserTrades")]
        public async Task<IActionResult> GetUserTrades(int? userid)
        {

            if (userid == null)
            {
                return NotFound();
            }

            var trades = await _repo.GetTrades(userid.Value);
            
            if( trades == null)
                return BadRequest("No data found");
            return Ok(trades);
        }


        [HttpGet("getusertrade/{id}", Name = "GetUserTrade")]
        public async Task<IActionResult> GetUserTrade(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trade = await _repo.GetTrade(id.Value);
            if (trade == null)
            {
                return NotFound();
            }

            return Ok(trade);
        }

 

    }
}

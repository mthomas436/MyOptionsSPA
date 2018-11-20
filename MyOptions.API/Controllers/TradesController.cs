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
    public class TradesController : ControllerBase
    {
/* 
        [AcceptVerbs("OPTIONS")]
        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Headers.Add("Access-Control-Allow-Origin", "*");
            resp.Headers.Add("Access-Control-Allow-Methods", "GET,DELETE");

            return resp;
        }
*/
        private readonly ITradeRepository _repo;
        public int currentUserId;        
        public TradesController(ITradeRepository repo, IMapper mapper)
        {
            _repo = repo;   
        }

        [HttpGet("gettrades/{userid}",Name="GetTrades")]
        public async Task<IActionResult> GetTrades(int? userid)
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


        [HttpGet("gettrade/{id}", Name = "GetTrade")]
        public async Task<IActionResult> GetTrade(int? id)
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


        [HttpPost("createtrade", Name = "CreateTrade")]
        [ProducesResponseType(201, Type = typeof(Trade))]
        public async Task<IActionResult> CreateTrade(TradeForInsertDto tradeForInsertDto)
        {
            if (tradeForInsertDto.Userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var tradeToInsert = Mapper.Map<Trade>(tradeForInsertDto);

            var createdTrade = await _repo.AddTrade(tradeToInsert);


            return Ok(createdTrade);
        }


        [HttpPost("updatetrade", Name = "UpdateTrade")]
        public async Task<IActionResult> UpdateTrade(TradeForUpdateDto tradeForUpdateDto)
        {

            if (tradeForUpdateDto.Userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            Trade tradeForUpdate = await _repo.GetTrade(tradeForUpdateDto.Tradeid);

            try
            {
                tradeForUpdate.Description = tradeForUpdateDto.Description;
                tradeForUpdate.Notes = tradeForUpdateDto.Notes;
                tradeForUpdate.StockSymbol = tradeForUpdateDto.StockSymbol.ToUpper();
                tradeForUpdate.TradeTypeId = tradeForUpdateDto.TradeTypeId;

                await _repo.UpdateTrade(tradeForUpdate);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TradeExists(tradeForUpdateDto.Tradeid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(tradeForUpdate);
        }


        [HttpDelete("deletetrade/{tradeid}", Name="DeleteTrade")]
        public async Task<IActionResult> DeleteTrade(int tradeid)
        {
           var tradeFromDb = await _repo.GetTrade(tradeid);

           if(tradeFromDb == null)
           {
               return NotFound();
           }

           if (tradeFromDb.Userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
               return Unauthorized();

            var trade = await _repo.DeleteTrade(tradeFromDb.Tradeid);

            return NoContent();
        }        

        private bool TradeExists(int id)
        {
            var trade = _repo.GetTrade(id);
            if (trade == null)
                return false;
            return true;
        }

    }
}

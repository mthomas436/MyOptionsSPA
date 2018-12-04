using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyOptions.API.Data;
using MyOptions.API.Dtos;
using MyOptions.API.Models;

namespace MyOptions.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]    
    public class OptionsController : ControllerBase
    {
        private readonly IOptionsRepository _repo;
        private readonly ITradeRepository _tradeRepo;
        private readonly IMapper _mapper;
 
        public OptionsController(IOptionsRepository repo, ITradeRepository tradeRepo, IMapper mapper)
        {
            _repo = repo;
            _tradeRepo = tradeRepo;
            _mapper = mapper;
        }

        [HttpGet("getoptions/{id}", Name = "GetOptions")]
        public async Task<IActionResult> GetOptions(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trade = await _repo.GetOptions(id.Value);
            if (trade == null)
            {
                return NotFound();
            }

            return Ok(trade);
        }


        [HttpGet("getoption/{optionid}", Name = "GetOption")]
        public async Task<IActionResult> GetOption(int? optionid)
        {
            if (optionid == null)
            {
                return NotFound();
            }

            var option = await _repo.GetOption(optionid.Value);
            if (option == null)
            {
                return NotFound();
            }

            return Ok(option);
        }



        [HttpPost("addoption",Name="AddOption")]
        public async Task<IActionResult> AddOption(Option option)
        {
            option.DateEntered = DateTime.Now;
            var newOption = await _repo.AddOption(option);
            return Ok(newOption);
        }


        [HttpPost("updateoption",Name="UpdateOption")]
        public async Task<IActionResult> UpdateOption(OptionForUpdateDto optionForUpdateDto)
        {
            var optionFromDb = await _repo.GetOption(optionForUpdateDto.Optionid);

            if(optionFromDb == null)
                return NotFound();
 

            if (optionFromDb.Trades.Userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            optionFromDb.Commission = optionForUpdateDto.Commission;
            optionFromDb.EntryPrice = optionForUpdateDto.EntryPrice;
            optionFromDb.ExpirationDate = optionForUpdateDto.ExpirationDate;
            optionFromDb.Notes = optionForUpdateDto.Notes;
            optionFromDb.OptionTypeId = optionForUpdateDto.OptionTypeId;
            optionFromDb.Quantity = optionForUpdateDto.Quantity;
            optionFromDb.StockPriceatPurchace = optionForUpdateDto.StockPriceatPurchace;
            optionFromDb.StrikePrice = optionForUpdateDto.StrikePrice;


            if (optionForUpdateDto.ExitPrice != null)
            {
                optionFromDb.ExitPrice = optionForUpdateDto.ExitPrice;
                optionFromDb.DateClosed = DateTime.Now;
            }
            

            var updatedOption = _repo.UpdateOption(optionFromDb);    

            return Ok(optionFromDb);
        }


        [HttpDelete("deleteoption/{optionid}", Name="DeleteOption")]
        public async Task<IActionResult> DeleteOption(int optionid)
        {
           var optionFromDb = await _repo.GetOption(optionid);

           if(optionFromDb == null)
           {
               return NotFound();
           }

           if (optionFromDb.Trades.Userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
               return Unauthorized();

            var tradeid = optionFromDb.Tradeid;

            var response = await _repo.DeleteOption(optionFromDb);

            var currentTrade = _tradeRepo.GetTrade(tradeid);
            return Ok(currentTrade.Result);
        }         
    }
}
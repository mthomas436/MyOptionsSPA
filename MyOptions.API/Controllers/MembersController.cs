using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyOptions.API.Data;

namespace MyOptions.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]   
    public class MembersController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;        
        public MembersController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;        
        }


        [HttpGet("getmembers", Name = "GetMembers")]
        public async Task<IActionResult> GetMembers()
        {

            var currentUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var users = await _repo.GetUsers(currentUser);
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }   

        [HttpGet("getmember/{id}", Name = "GetMember")]
        public async Task<IActionResult> GetMember(int? userid)
        {

            if (userid == null)
            {
                return NotFound();
            }

            var user = await _repo.GetUser(userid.Value);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }              
    }
}
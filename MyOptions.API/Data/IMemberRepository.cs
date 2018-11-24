using System.Collections.Generic;
using System.Threading.Tasks;
using MyOptions.API.Models;

namespace MyOptions.API.Data
{

    public interface IMemberRepository
    {
        Task<IEnumerable<User>> GetMembers(int curuserid);
        Task<User> GetMember(int userid); 

        Task<User> GetMemberTrades(int userid);
    }    
}
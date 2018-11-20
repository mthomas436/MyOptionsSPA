using System.Collections.Generic;
using System.Threading.Tasks;
using MyOptions.API.Models;

namespace MyOptions.API.Data
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers(int curuserid);
        Task<User> GetUser(int userid); 
    }
}
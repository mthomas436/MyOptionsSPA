using System.Collections.Generic;
using System.Threading.Tasks;
using MyOptions.API.Models;

namespace MyOptions.API.Data
{
    public interface ITestRepository
    {
        Task<IEnumerable<Trade>> GetTrades(int userid);
        Task<Trade> GetTrade(int tradeid);    
    }
}
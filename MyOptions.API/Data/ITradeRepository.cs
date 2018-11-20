using System.Collections.Generic;
using System.Threading.Tasks;
using MyOptions.API.Models;

namespace MyOptions.API.Data
{
    public interface ITradeRepository
    {
        Task<IEnumerable<Trade>> GetTrades(int userid);
        Task<Trade> GetTrade(int tradeid);
        Task<Trade> AddTrade(Trade trade);
        Task<string> DeleteTrade(int id);
        Task<Trade> UpdateTrade(Trade trade);
        Task<IEnumerable<TradeType>> GetTradeTypes();
        Task<IEnumerable<Option>> GetOptionDetail(int tradeid);    
    }
}
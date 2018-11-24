using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyOptions.API.Models;

namespace MyOptions.API.Data
{
    public class TestRepository : ITestRepository
    {
        private readonly DataContext _context;
        public TestRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Trade> GetTrade(int tradeid)
        {
            var trade = await _context.Trades
                          .Where(t => t.Tradeid == tradeid)
                          .Include(t => t.TradeTypes)
                          .Include(o => o.Options)
                          .ThenInclude(ot => ot.OptionType)
                          .OrderByDescending(o => o.DateEntered)
                          .FirstOrDefaultAsync();

            return trade;
        }  

        public async Task<IEnumerable<Trade>> GetTrades(int userid)
        {
            var trades = await _context.Trades
                         .Where(t => t.Userid == userid)
                         .Include(t => t.TradeTypes)                         
                         .ToListAsync();

            return trades;
        }          
    }
}
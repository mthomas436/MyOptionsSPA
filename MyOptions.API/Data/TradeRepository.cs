using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyOptions.API.Models;

namespace MyOptions.API.Data
{
    public class TradeRepository : ITradeRepository
    {
        private readonly DataContext _context;
        public TradeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Trade> AddTrade(Trade trade)
        {
            await _context.Trades.AddAsync(trade);

            await _context.SaveChangesAsync();

            return trade;
        }

        public async Task<string> DeleteTrade(int id)
        {
            var trade = await GetTrade(id);
            _context.Trades.Remove(trade);

            await _context.SaveChangesAsync();

            return "The record has been deleted successfully.";
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

        public async Task<IEnumerable<Option>> GetOptionDetail(int tradeid)
        {
            var option = await _context.Options
                         .Where(t => t.Tradeid == tradeid)
                         .Include(T => T.Trades)
                         .Include(y => y.OptionType)
                         .Include(p => p.Trades.TradeTypes)
                         .OrderByDescending(o => o.DateEntered)
                         .ToListAsync();
 
            return option;
        }

        public async Task<IEnumerable<Trade>> GetTrades(int userid)
        {
            var trades = await _context.Trades
                         .Where(t => t.Userid == userid)
                         .Include(t => t.TradeTypes)                         
                         .ToListAsync();

            return trades;
        }

        public async Task<Trade> UpdateTrade(Trade trade)
        {
            _context.Trades.Update(trade);

            await _context.SaveChangesAsync();

            return trade;
        }

        public async Task<IEnumerable<TradeType>> GetTradeTypes()
        {
            return await _context.TradeTypes.ToListAsync();
        }
 
    }
}
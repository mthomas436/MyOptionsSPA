using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyOptions.API.Models;

namespace MyOptions.API.Data
{
    public class OptionRepository : IOptionsRepository
    {
        private readonly DataContext _context;
        public OptionRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Option> UpdateOption(Option option)
        {
            _context.Options.Update(option);
            await _context.SaveChangesAsync();

            return option;
        }
  
        public async Task<Option> AddOption(Option option)
        {
            await _context.Options.AddAsync(option);

            await _context.SaveChangesAsync();

            return option;
        }
 
        public async Task<Option> GetOption(int optionid)
        {
            var option = await _context.Options
                         .Where(o => o.Optionid == optionid)
                         .Include(ot => ot.OptionType)
                         .Include(t => t.Trades)
                         .Include(t => t.Trades.TradeTypes)
                         .FirstOrDefaultAsync();

            return option;
        }

        public async Task<IEnumerable<Option>> GetOptions(int tradeid)
        {
            var options = await _context.Options
                          .Where(t => t.Tradeid == tradeid)
                          .ToListAsync();
            return options;
        }
        
        public async Task<string> DeleteOption(Option option)
        {
            _context.Options.Remove(option);
            await _context.SaveChangesAsync();
            return "The record has been deleted successfully.";
        }        
        
/* 
        public async Task<IEnumerable<Option>> GetOptions(int tradeid)
        {
            var option = await _context.Options
                        .Where(t => t.Tradeid == tradeid)
                        .Include(o => o.OptionType)
                        .ToListAsync();

            return option;
        }
*/
 

 
/* 
        public async Task<Trade> GetTrade(int tradeid)
        {
            var trade = await _context.Trades
                        .Where(t => t.Tradeid == tradeid)
                        .Include(t => t.TradeTypes)
                        .FirstOrDefaultAsync();

            return trade;
        }
*/

/* 
        public async Task<IEnumerable<OptionType>> getOptionTypes()
        {
            return await _context.OptionTypes.ToListAsync();
        }  
        */
    }
}
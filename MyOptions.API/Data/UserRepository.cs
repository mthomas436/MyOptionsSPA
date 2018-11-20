using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyOptions.API.Models;

namespace MyOptions.API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

  

        public async Task<User> GetUser(int userid)
        {
            var user = await _context.Users
                       .Where(u => u.Userid == userid)
                       .Include(t => t.Trades)
                       .FirstOrDefaultAsync();

            return user;
          
        }

        public async Task<IEnumerable<User>> GetUsers(int curuserid)
        {
            var users = await _context.Users
                        .Where(u => u.Userid != curuserid)
                        .Include(t => t.Trades)
                        .ToListAsync();

            return users;

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
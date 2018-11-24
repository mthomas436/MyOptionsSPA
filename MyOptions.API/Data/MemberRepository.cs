using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyOptions.API.Models;

namespace MyOptions.API.Data
{
  public class MemberRepository : IMemberRepository
  {
    private readonly DataContext _context;
    public MemberRepository(DataContext context)
    {
      _context = context;
    }

    public async Task<User> GetMember(int userid)
    {
      var user = await _context.Users
                 .Where(u => u.Userid == userid)
                 .Include(t => t.Trades)
                 .FirstOrDefaultAsync();

      return user;

    }

    public async Task<IEnumerable<User>> GetMembers(int curuserid)
    {
      return (await _context.Users
               .Where(u => u.Userid != curuserid)
               .Include(t => t.Trades)
               .ToListAsync()
             );
    }

    public async Task<User> GetMemberTrades(int userid)
    {
      return (await _context.Users
               .Where(u => u.Userid == userid)
               .Include(t => t.Trades)
               .ThenInclude(t => t.TradeTypes)
               .FirstOrDefaultAsync()
             );
    }

  }
}
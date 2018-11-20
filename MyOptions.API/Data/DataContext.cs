using Microsoft.EntityFrameworkCore;
using MyOptions.API.Models;

namespace MyOptions.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>  options) : base (options) {}
        public DbSet<User> Users { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<TradeType> TradeTypes { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionType> OptionTypes { get; set; }

    }
}
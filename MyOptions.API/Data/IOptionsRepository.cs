using System.Collections.Generic;
using System.Threading.Tasks;
using MyOptions.API.Models;

namespace MyOptions.API.Data
{
    public interface IOptionsRepository
    {
        Task<Option> AddOption(Option option);
        Task<Option> GetOption(int optionid);

        Task<IEnumerable<Option>> GetOptions(int tradeid);
        Task<string> DeleteOption(Option option);
        Task<Option> UpdateOption(Option stock);
        //Task<Trade> GetTrade(int tradeid);
        //Task<IEnumerable<OptionType>> getOptionTypes();   
    }
}
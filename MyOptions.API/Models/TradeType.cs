using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyOptions.API.Models
{
    public class TradeType
    {
        public int TradeTypeId { get; set; }

        [Display(Name="Description")]
        public string Description { get; set; }

        public ICollection<Trade> Trades { get; set; }   
    }
}
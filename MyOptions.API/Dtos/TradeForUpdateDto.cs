using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOptions.API.Dtos
{
    public class TradeForUpdateDto
    {
        public int Tradeid { get; set; }

        public string Description { get; set; }
        public string Notes { get; set; }

        public string StockSymbol { get; set; }

        public DateTime DateEntered { get; set; }

        public int Userid { get; set; }
        public int TradeTypeId { get; set; }
    }
}

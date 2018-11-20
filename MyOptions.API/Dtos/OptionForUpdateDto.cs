using System;

namespace MyOptions.API.Dtos
{
    public class OptionForUpdateDto
    {
        public int Optionid { get; set; }
        public int OptionTypeId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double StrikePrice { get; set; }
        public int Quantity { get; set; }
        public double EntryPrice { get; set; }
        public double StockPriceatPurchace { get; set; }
        public double? ExitPrice { get; set; }
        public double? Commission { get; set; }
        public string Notes { get; set; }
        public DateTime? DateClosed { get; set; }

    }
}
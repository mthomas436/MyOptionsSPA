using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyOptions.API.Models
{
  public class Trade
  {
        [Display(Name="Id")]
        public int Tradeid { get; set; }

        [Display(Name="Description")]
        public string Description { get; set; }

        [Display(Name="Notes")]
        public string Notes { get; set; }

        [Required]
        [Display(Name="Stock Symbol")]
        public string StockSymbol { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name="Date Entered")]   
        public DateTime DateEntered { get; set; }

        [Display(Name="Date Closed")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateClosed { get; set; }


        public int Userid { get; set; }

        [Display(Name = "Trade Type")]
        public int TradeTypeId { get; set; }

        public TradeType TradeTypes { get; set; }

        public ICollection<Option> Options { get; set; }
  }
}
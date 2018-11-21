using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyOptions.API.Models
{
    public class Option
    {
        [Display(Name="Id")]
        public int Optionid { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [Display(Name="Strike Price")]
        public double StrikePrice { get; set; }

        [Required]
        [Display(Name="Quantity")]    
        [Range(1,int.MaxValue,ErrorMessage ="Quantity shouldbe greater than 0")]
        public int Quantity { get; set; }


        [Required]
        [Display(Name="Date Entered")]
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateEntered { get; set; }


        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Option Price should be greater than 0")]
        [Display(Name= "Option Price at Entry")]
        public double EntryPrice { get; set; }


        [Required]
        [Display(Name="Stock Price at Purchase")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Stock Price at Purchase should be greater than 0")]
        public double StockPriceatPurchace { get; set; }

        [Display(Name="Exit Price")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Exit Price should be greater than 0")]
        public double? ExitPrice { get; set; }


        [Display(Name="Commission")]
        public double? Commission { get; set; }

        [Display(Name="Date Closed")]
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateClosed { get; set; }


        [Display(Name="Notes")]
        public string Notes { get; set; }

        public Trade Trades { get; set; }
        public int Tradeid { get; set; }


        public OptionType OptionType { get; set; }

        [Display(Name = "Option Type")]
        public int OptionTypeId { get; set; }

        [NotMapped]
        [Display(Name = "Option Cost")]
        public double? OptionCost
        {
            get
            {
                return (EntryPrice * 100 * Quantity) + (Commission == null ? 0 : Commission);
            }
        }

        [NotMapped]
        [Display(Name = "Closing Price")]
        public double? ClosingTotalPrice
        {
            get
            {
                return (ExitPrice * 100 * Quantity) + (Commission == null ? 0 : Commission);
            }
        }

        [NotMapped]
        [Display(Name = "Profit Loss")]
        public double? ProfitLoss
        {
            get
            {
                return ClosingTotalPrice-OptionCost;
            }
        } 

        [NotMapped]
        [Display(Name = "Exp Date")]
        public string ExpDateShort
        {
            get
            {
                return ExpirationDate.ToString("MM/dd/yyyy");
            }
        }   

        [NotMapped]
        [Display(Name = "Position Closed")]
        public Boolean PositionClosed
        {
            get
            {
                return DateClosed == null ? false : true;
            }
        }

    }
}
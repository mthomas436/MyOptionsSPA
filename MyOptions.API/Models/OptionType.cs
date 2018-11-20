using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyOptions.API.Models
{
    public class OptionType
    {
        [Display(Name = "Type")]
        public int OptionTypeId { get; set; }

        [Display(Name="Description")]
        public string Description { get; set; }

        public ICollection<Option> Options { get; set; }
    }
}
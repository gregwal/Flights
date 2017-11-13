using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Flights.Utility;

namespace Flights.ViewModels
{
    public class CurrencyViewModel
    {

        [Required]
        [Display(Name = "To Currency")]
        public string To { get; set; }

        public IEnumerable<SelectListItem> ToSelectItems { get; set; }
        public Dictionary<string, decimal> UsdConversionRates { get; set; }
        public IDictionary<string, string> CurrencySymbols { get; set; }

        [Required]
        [Display(Name = "US dollar amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Converted Foreign Currency Amount")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public string ConvertedAmount { get ; set; }
    }
}
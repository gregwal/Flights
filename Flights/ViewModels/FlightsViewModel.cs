using Flights.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Flights.ViewModels
{
    public class FlightsViewModel
    {
        [Required]
        [Display(Name = "From Airport")]
        public string From { get; set; }

        [Required]
        [Display(Name = "To Airport")]
        public string To { get; set; }

        public IEnumerable<SelectListItem> FromSelectItems { get; set; }
        public IEnumerable<SelectListItem> ToSelectItems { get; set; }

        public List<Flight> Flights { get; set; }
        
    }
}
using Flights.Data;
using Flights.Models;
using Flights.ViewModels;
using Flights.Utility;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Flights.Controllers
{
    public class HomeController : Controller
    {

        private IRepository _repository;


        public HomeController(IRepository repository)
        {
            this._repository = repository;
        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult FindFlights()
        {
            FlightsViewModel flm = new FlightsViewModel();
            var airports = _repository.GetAirports();
            flm.FromSelectItems = airports.Select(a => new SelectListItem { Text = a.Name, Value = a.Code }).ToList();
            flm.ToSelectItems = airports.Select(a => new SelectListItem { Text = a.Name, Value = a.Code }).ToList();
            return View(flm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ViewResult FindFlights(FlightsViewModel flm)
        {
            if (flm == null)
            {
                flm = new FlightsViewModel();
            }

            var airports = _repository.GetAirports();
            flm.FromSelectItems = airports.Select(a => new SelectListItem { Text = a.Name, Value = a.Code }).ToList();
            flm.ToSelectItems = airports.Select(a => new SelectListItem { Text = a.Name, Value = a.Code }).ToList();

            if (ModelState.IsValid)
            {
                flm.Flights = _repository.GetFlights(flm.From, flm.To);
            }
            return View(flm);
        }

        public async Task<ViewResult> CurrencyConverter()
        {
            CurrencyViewModel cvm = new CurrencyViewModel();
            var currencies = await _repository.GetCurrenciesAsync();

            cvm.ToSelectItems = currencies.Select(a => new SelectListItem { Text = a.Value, Value = a.Key}).OrderBy(x => x.Text).ToList();
            cvm.Amount = 0;
            cvm.ConvertedAmount = "";

            return View(cvm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ViewResult> CurrencyConverter(CurrencyViewModel cvm)
        {
            if (cvm == null)
            {
                cvm = new CurrencyViewModel();
            }

            var currencies = await _repository.GetCurrenciesAsync();
            cvm.ToSelectItems = currencies.Select(a => new SelectListItem { Text = a.Value, Value = a.Key}).ToList();
            cvm.UsdConversionRates = await _repository.GetCurrencyConversionsAsync();
            //cvm.CurrencySymbols = Utility.Utility.GetCurrencySymbols();

            decimal toConversionRate = 1; 

            if (ModelState.IsValid)
            {
                cvm.UsdConversionRates.TryGetValue("USD" + cvm.To, out toConversionRate);
                cvm.ConvertedAmount = (toConversionRate * cvm.Amount).FormatCurrency(cvm.To) ;
            }
            return View(cvm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
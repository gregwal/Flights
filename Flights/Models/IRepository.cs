using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flights.Models
{
    public interface IRepository
    {
        List<Airport> GetAirports();
        List<Flight> GetFlights(string From, string To);

        Task<Dictionary<string, string>> GetCurrenciesAsync();
        Task<Dictionary<string, decimal>> GetCurrencyConversionsAsync();
    }
}

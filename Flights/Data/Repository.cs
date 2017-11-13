using Flights.Models;
using CsvHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Flights.Data
{
    public class Repository : IRepository
    {
        private static string airportCsvPath = ConfigurationManager.AppSettings["airportPath"];
        private static string flightCsvPath = ConfigurationManager.AppSettings["flightPath"];
        string baseUrl = "http://www.apilayer.net/";
        string accessKey = ConfigurationManager.AppSettings["currencyAccessKey"];

        public List<Airport> GetAirports()
        {
            try
            {
                using (TextReader filereader = File.OpenText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, airportCsvPath)))
                {
                    var csv = new CsvReader(filereader);
                    return csv.GetRecords<Airport>().ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Flight> GetFlights(string from, string to)
        {
            try
            {
                using (TextReader filereader = File.OpenText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, flightCsvPath)))
                {
                    var csv = new CsvReader(filereader);
                    return csv.GetRecords<Flight>().Where(a => a.From == from && a.To == to).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Dictionary<string, string>> GetCurrenciesAsync()
        {
            Dictionary<string, string> currencyInfo = new Dictionary<string, string>();

            using (HttpClient client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage response = await client.GetAsync("api/list?" + accessKey);

                //Checking the response is successful or not which is sent using HttpClient  
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var currencyResponse = response.Content.ReadAsStringAsync().Result;
                    var root = JObject.Parse(currencyResponse);
                    currencyInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(root["currencies"].ToString());
                }
                //returning the employee list to view  
                return currencyInfo;
            }
        }

        public async Task<Dictionary<string, decimal>> GetCurrencyConversionsAsync()
        {
            Dictionary<string, decimal> currencyConversionInfo = new Dictionary<string, decimal>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/live?" + accessKey);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var currencyResponse = Res.Content.ReadAsStringAsync().Result;
                    var root = JObject.Parse(currencyResponse);

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    currencyConversionInfo = JsonConvert.DeserializeObject<Dictionary<string, decimal>>(root["quotes"].ToString());
                }
                //returning the employee list to view  
                return currencyConversionInfo;
            }
        }
    }
}
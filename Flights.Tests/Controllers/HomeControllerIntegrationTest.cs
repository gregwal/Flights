using Flights.Controllers;
using Flights.Data;
using Flights.Models;
using Flights.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;

namespace Flights.Tests.Controllers
{
    [TestClass]
    public class HomeControllerIntegrationTest
    {
        [TestMethod]
        public void Integrate_SearchFlights_PageLoad_HasMoreThanOneFromSelectItems()
        {
            // Arrange
            IRepository repository = new Repository();
            HomeController controller = new HomeController(repository);

            // Act
            ViewResult result = controller.FindFlights() as ViewResult;
            var fvm = (FlightsViewModel)result.Model;

            // Assert
            Assert.IsTrue(fvm.FromSelectItems.Count() > 0);
        }

        [TestMethod]
        public void ConvertCurrency_GetCurrencies_MoreThanOneCurrency()
        {
            // Arrange
            IRepository repository = new Repository();

            // Act
            var currencies = repository.GetCurrenciesAsync();

            // Assert
            Assert.IsTrue(currencies.Result.Count > 0);

        }

        [TestMethod]
        public void ConvertCurrency_GetCurrencyConversion_MoreThanOneCurrencyConversion()
        {
            // Arrange
            IRepository repository = new Repository();

            // Act
            var currencyConversions = repository.GetCurrencyConversionsAsync();

            // Assert
            Assert.IsTrue(currencyConversions.Result.Count > 0);

        }
    }
}

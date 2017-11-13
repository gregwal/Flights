using Flights.Controllers;
using Flights.Models;
using Flights.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Telerik.JustMock;

namespace Flights.Tests.Controllers
{
    [TestClass]
    public class HomeControllerUnitTest
    {
        //- User should be able to search for flights between two different airports.
        [TestMethod]
        public void SearchFlights_PageLoad_HasMoreThanOneFromSelectItems()
        {
            // Arrange
            var repository = Mock.Create<IRepository>();
            Mock.Arrange(() => repository.GetAirports()).
                Returns(new List<Airport>()
                {
                    new Airport {Code="SEA", Name="Seattle WA (SEA-Seattle/Tacoma Intl.)" },
                    new Airport {Code="LAS", Name="Las Vegas NV (LAS-McCarran Intl.)" },
                    new Airport {Code="SEA", Name="Los Angeles CA (LAX-Los Angeles Intl.)" },
                    new Airport {Code="LAX", Name="Phoenix AZ (PHX-Sky Harbor Intl.)" },
                }).MustBeCalled();

            FlightsViewModel fvm = new FlightsViewModel() { From = "SEA", To = "LAS" };

            // Act
            HomeController controller = new HomeController(repository);
            ViewResult result = controller.FindFlights(fvm);
            var airports = (FlightsViewModel)result.ViewData.Model;

            // Assert
            Assert.IsTrue(fvm.FromSelectItems.Count() > 0);
        }


        //- User should be able to search for flights between two different airports.
        [TestMethod]
        public void SearchFlights_PageLoad_HasMoreThanOneToSelectItems()
        {
            var repository = Mock.Create<IRepository>();
            Mock.Arrange(() => repository.GetAirports()).
                Returns(new List<Airport>()
                {
                    new Airport {Code="SEA", Name="Seattle WA (SEA-Seattle/Tacoma Intl.)" },
                    new Airport {Code="LAS", Name="Las Vegas NV (LAS-McCarran Intl.)" },
                    new Airport {Code="SEA", Name="Los Angeles CA (LAX-Los Angeles Intl.)" },
                    new Airport {Code="LAX", Name="Phoenix AZ (PHX-Sky Harbor Intl.)" },
                }).MustBeCalled();

            FlightsViewModel fvm = new FlightsViewModel() { From = "SEA", To = "LAS" };

            // Act
            HomeController controller = new HomeController(repository);
            ViewResult result = controller.FindFlights(fvm);
            var airports = (FlightsViewModel)result.ViewData.Model;

            // Assert
            Assert.IsTrue(fvm.ToSelectItems.Count() > 0);
        }

        // User should be able to see a list of flights matching the search parameters on the previous step
        [TestMethod]
        public void SearchFlights_TwoDifferentAirports_Return4Flights()
        {
            // Arrange
            var repository = Mock.Create<IRepository>();
            Mock.Arrange(() => repository.GetFlights("SEA", "LAS")).
                Returns(new List<Flight>()
                {
                    new Flight {From="SEA", To="LAS", FlightNumber=1000, Arrives="6:00 PM", Departs="8:00 PM", MainCabinPrice=100.00m, FirstClassPrice=200.00m},
                    new Flight {From="SEA", To="LAS", FlightNumber=1001, Arrives="7:00 PM", Departs="9:00 PM", MainCabinPrice=110.00m, FirstClassPrice=190.00m},
                    new Flight {From="SEA", To="LAS", FlightNumber=1002, Arrives="4:00 PM", Departs="6:00 PM", MainCabinPrice=99.00m, FirstClassPrice=175.00m},
                    new Flight {From="SEA", To="LAS", FlightNumber=1003, Arrives="7:00 AM", Departs="9:00 AM", MainCabinPrice=132.00m, FirstClassPrice=214.00m},
                }).MustBeCalled();

            FlightsViewModel fvm = new FlightsViewModel() { From = "SEA", To = "LAS" };

            // Act
            HomeController controller = new HomeController(repository);
            ViewResult result = controller.FindFlights(fvm);
            var flights = (FlightsViewModel) result.ViewData.Model;
            // Assert
            Assert.AreEqual(4, flights.Flights.Count());
        }
    }
}

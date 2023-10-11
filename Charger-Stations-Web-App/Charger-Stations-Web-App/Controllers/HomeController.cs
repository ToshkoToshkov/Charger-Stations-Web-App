namespace Charger_Stations_Web_App.Controllers
{
    using Charger_Stations_Web_App.Data;
    using Charger_Stations_Web_App.Models;
    using Charger_Stations_Web_App.Models.Home;
    using Charger_Stations_Web_App.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly ChargerStationsDbContext data;

        public HomeController(IStatisticsService statistics, ChargerStationsDbContext data)
        {
            this.data = data;
            this.statistics = statistics;
        }

        public IActionResult Index() 
        {
            var chargers = this.data
                .Chargers
                .OrderByDescending(c => c.Id)
                .Select(c => new ChargerIndexViewModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    ImageURL = c.ImageURL,
                    PricePerHour = c.PricePerHour,
                    LocationUrl = c.LocationUrl,
                    Category = c.Category.Name
                })
                .Take(3)
                .ToList();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalChargers = totalStatistics.TotalChargers,
                TotalUsers = totalStatistics.TotalUsers,
                Chargers = chargers
            });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
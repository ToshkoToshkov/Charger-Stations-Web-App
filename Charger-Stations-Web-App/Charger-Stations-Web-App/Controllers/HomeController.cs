namespace Charger_Stations_Web_App.Controllers
{
    using Charger_Stations_Web_App.Data;
    using Charger_Stations_Web_App.Models;
    using Charger_Stations_Web_App.Models.Home;
    using Charger_Stations_Web_App.Services.Chargers;
    using Charger_Stations_Web_App.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly IChargersService Chargers;

        public HomeController(IChargersService chargers, IStatisticsService statistics)
        {
            this.Chargers = chargers;
            this.statistics = statistics;
        }

        public IActionResult Index() 
        {
            var latestChargers = this.Chargers
                .Latest()
                .ToList();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalChargers = totalStatistics.TotalChargers,
                TotalUsers = totalStatistics.TotalUsers,
                Chargers = latestChargers
            });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
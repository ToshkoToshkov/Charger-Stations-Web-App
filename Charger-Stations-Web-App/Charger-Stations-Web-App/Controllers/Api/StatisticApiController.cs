namespace Charger_Stations_Web_App.Controllers.Api
{
    using Charger_Stations_Web_App.Data;
    using Charger_Stations_Web_App.Models.Api.Statistics;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticApiController : ControllerBase
    {
        private readonly ChargerStationsDbContext data;

        public StatisticApiController(ChargerStationsDbContext data)
            => this.data = data;

        [HttpGet]
        public StatisticsResponseModel GetStatistics()
        {
            var totalChargers = this.data.Chargers.Count();
            var totalUsers = this.data.Users.Count();

            var statistics = new StatisticsResponseModel
            {
                TotalChargers = totalChargers,
                TotalUsers = totalUsers,
            };

            return statistics;
        }

    }
}

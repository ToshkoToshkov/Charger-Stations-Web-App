namespace Charger_Stations_Web_App.Controllers.Api
{
    using Charger_Stations_Web_App.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticApiController : ControllerBase
    {
        private readonly IStatisticsService statistics;

        public StatisticApiController(IStatisticsService statistics)
            => this.statistics = statistics;

        [HttpGet]
        public StatisticsServiceModel GetStatistics()
            => this.statistics.Total();

    }
}

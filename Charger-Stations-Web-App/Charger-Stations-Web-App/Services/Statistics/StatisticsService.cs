namespace Charger_Stations_Web_App.Services.Statistics
{
    using Charger_Stations_Web_App.Data;

    public class StatisticsService : IStatisticsService
    {
        private readonly ChargerStationsDbContext data;

        public StatisticsService(ChargerStationsDbContext data) 
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalChargers = this.data.Chargers.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalChargers = totalChargers,
                TotalUsers = totalUsers
            };
        }
    }
}

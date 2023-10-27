using Charger_Stations_Web_App.Data;

namespace Charger_Stations_Web_App.Services.Dealers
{
    public class DealerService : IDealerService
    {
        private readonly ChargerStationsDbContext data;

        public DealerService(ChargerStationsDbContext data) 
            => this.data = data;
       
        public bool IsDealer(string userId) 
            => this.data.Dealers.Any(d => d.UserId == userId);
    }
}

namespace Charger_Stations_Web_App.Services.Chargers
{
    public class ChargersQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int ChargersPerPage { get; init; }

        public int TotalChargers { get; init; }

        public IEnumerable<ChargerServiceModel> Chargers { get; init; }

    }
}

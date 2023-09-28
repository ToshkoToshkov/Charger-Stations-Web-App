namespace Charger_Stations_Web_App.Models.Home
{
    public class IndexViewModel
    {

        public int TotalChargers { get; init; }

        public int TotalUsers { get; init; }

        public List<ChargerIndexViewModel> Chargers { get; init; }
    }
}

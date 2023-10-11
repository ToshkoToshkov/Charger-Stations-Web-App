namespace Charger_Stations_Web_App.Models.Api.Chargers
{
    using Charger_Stations_Web_App.Models;

    public class AllChargersApiRequestModel
    {
        public string Model { get; init; }

        public string SearchTerm { get; init; }

        public AllChargersSorting Sorting { get; set; }

        public int CurrentPage { get; init; } = 1;

        public int ChargersPerPage { get; init; }

        public int TotalChargers { get; init; }

    }
}

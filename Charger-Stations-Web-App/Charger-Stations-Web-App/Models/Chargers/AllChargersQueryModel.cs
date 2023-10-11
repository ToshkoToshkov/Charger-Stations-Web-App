namespace Charger_Stations_Web_App.Models.Chargers
{
    using Charger_Stations_Web_App.Models;
    using Charger_Stations_Web_App.Services.Chargers;
    using System.ComponentModel.DataAnnotations;
    public class AllChargersQueryModel
    {
        public const int ChargersPerPage = 3; 

        public string Model { get; init; }

        public IEnumerable<string> Models { get; init; }

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }

        public AllChargersSorting Sorting { get; set; }

        public int CurrentPage { get; init; } = 1;

        public int TotalChargers { get; set; }

        public IEnumerable<ChargerServiceModel> Chargers { get; set; }

        public string Category { get; set; }

        public IEnumerable<string> Categories { get; set; }


    }
}

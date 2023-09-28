namespace Charger_Stations_Web_App.Models.Chargers
{
    using System.ComponentModel.DataAnnotations;
    public class AllChargersQueryModel
    {
        public string Model { get; init; }

        public IEnumerable<string> Models { get; init; }

        [Display(Name = "Search")]
        public string SearchTerm { get; init; }

        public AllChargersSorting Sorting { get; init; }

        public IEnumerable<ChargerListingViewModel> Chargers { get; init; }

        public string Category { get; set; }

        public IEnumerable<string> Categories { get; set; }

    }
}

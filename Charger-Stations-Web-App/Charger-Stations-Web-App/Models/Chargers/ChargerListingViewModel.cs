namespace Charger_Stations_Web_App.Models.Chargers
{
    using Charger_Stations_Web_App.Data.Models;

    public class ChargerListingViewModel
    {
        public int Id { get; init; }

        public string Model { get; init; }

        public string ImageURL { get; init; }

        public decimal? PricePerHour { get; init; }

        public string Owner { get; init; }

        public string LocationUrl { get; init; }

        public string Category { get; init; }
    }
}

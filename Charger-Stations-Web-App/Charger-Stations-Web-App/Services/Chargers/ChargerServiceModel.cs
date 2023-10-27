namespace Charger_Stations_Web_App.Services.Chargers
{
    public class ChargerServiceModel
    {
        public int Id { get; init; }

        public string Model { get; init; }

        public string ImageURL { get; init; }

        public decimal? PricePerHour { get; init; }

        public string LocationUrl { get; init; }

        public string Category { get; init; }
    }
}

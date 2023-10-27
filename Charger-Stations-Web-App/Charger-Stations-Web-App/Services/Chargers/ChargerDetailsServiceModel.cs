namespace Charger_Stations_Web_App.Services.Chargers
{
    public class ChargerDetailsServiceModel : ChargerServiceModel
    {
        public string Description { get; init; }

        public int DealerId { get; init; }

        public string DealerName { get; init; }

        public int CategoryId { get; init; }

        public string UserId { get; init; }
    }
}

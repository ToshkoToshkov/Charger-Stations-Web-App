namespace Charger_Stations_Web_App.Services.Chargers
{
    using Charger_Stations_Web_App.Models;

    public interface IChargersService
    {
        ChargersQueryServiceModel All(
            string model,
            string searchTerm,
            AllChargersSorting sorting,
            int currentPage,
            int chargersPerPage);

        ChargerDetailsServiceModel Details(int id);

        int Create(
                string model,
                string description,
                string imageURL,
                decimal? pricePerHour,
                string locationUrl,
                int? categoryId,
                int dealerId);

        bool Edit(
                int id,
                string model,
                string description,
                string imageURL,
                decimal? pricePerHour,
                string locationUrl,
                int? categoryId,
                int dealerId);

        bool Delete(int id);

        IEnumerable<ChargerServiceModel> ByUser(string userId);

        IEnumerable<string> AllChargerCategories();

        bool CategoryExist(int? categoryId);
    }
}

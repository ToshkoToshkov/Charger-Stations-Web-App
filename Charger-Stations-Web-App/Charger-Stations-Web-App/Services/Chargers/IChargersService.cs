namespace Charger_Stations_Web_App.Services.Chargers
{
    using Charger_Stations_Web_App.Models;

    public interface IChargersService
    {
        ChargersQueryServiceModel All(string model, string searchTerm, AllChargersSorting sorting, int currentPage, int chargersPerPage);

        IEnumerable<string> AllChargerCategories();
    }
}

namespace Charger_Stations_Web_App.Services.Chargers
{
    using Charger_Stations_Web_App.Data;
    using Charger_Stations_Web_App.Models;
    using System.Collections.Generic;

    public class ChargersService : IChargersService
    {
        private readonly ChargerStationsDbContext data;

        public ChargersService(ChargerStationsDbContext data)
            => this.data = data;

        public ChargersQueryServiceModel All(string model, string searchTerm, AllChargersSorting sorting, int currentPage, int chargersPerPage)
        {
            var chargersQuery = this.data.Chargers.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                chargersQuery = chargersQuery.Where(c =>
                c.Model.ToLower().Contains(searchTerm.ToLower()) ||
                    c.Description.ToLower().Contains(searchTerm.ToLower()) ||
                    c.Category.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            chargersQuery = sorting switch
            {
                AllChargersSorting.Model => chargersQuery.OrderByDescending(c => c.Id),
                _ => chargersQuery.OrderByDescending(c => c.Id)
            };
            var totalChargers = chargersQuery.Count();

            var chargers = chargersQuery
                .Skip((currentPage - 1) * chargersPerPage)
                .Take(chargersPerPage)
                .Select(c => new ChargerServiceModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    ImageURL = c.ImageURL,
                    PricePerHour = c.PricePerHour,
                    LocationUrl = c.LocationUrl,
                    Category = c.Category.Name
                })
                .ToList();

            return new ChargersQueryServiceModel
            {
                TotalChargers = totalChargers,
                CurrentPage = currentPage,
                ChargersPerPage = chargersPerPage,
                Chargers = chargers
            };
        }

        public IEnumerable<string> AllChargerCategories()
            => this.data
                .Chargers
                .Select(c => c.Category.Name)
                .Distinct()
                .ToList();
    }
}

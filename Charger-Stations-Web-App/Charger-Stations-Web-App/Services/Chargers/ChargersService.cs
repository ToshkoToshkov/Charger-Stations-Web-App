namespace Charger_Stations_Web_App.Services.Chargers
{
    using Charger_Stations_Web_App.Data;
    using Charger_Stations_Web_App.Data.Models;
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

            var chargers = GetChargers(chargersQuery
                .Skip((currentPage - 1) * chargersPerPage)
                .Take(chargersPerPage));
                

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

        public IEnumerable<ChargerServiceModel> ByUser(string userId)
             => this.GetChargers(this.data
                 .Chargers
                 .Where(c => c.Dealer.UserId == userId));

        public bool CategoryExist(int? categoryId)
            => this.data
            .Categories
            .Any(c => c.Id == categoryId);

        public int Create(string model, string description, string imageURL, decimal? pricePerHour, string locationUrl, int? categoryId, int dealerId)
        {
            var chargerData = new Charger
            {
                Model = model,
                Description = description,
                ImageURL = imageURL,
                PricePerHour = pricePerHour,
                LocationUrl = locationUrl,
                CategoryId = categoryId,
                DealerId = dealerId
            };

            this.data.Chargers.Add(chargerData);
            this.data.SaveChanges();

            return chargerData.Id;
        }

        public ChargerDetailsServiceModel Details(int id)
            => this.data
            .Chargers
            .Where(c => c.Id == id)
            .Select(c => new ChargerDetailsServiceModel
            {
                Id = c.Id,
                Model = c.Model,
                Description = c.Description,
                ImageURL = c.ImageURL,
                PricePerHour = c.PricePerHour,
                LocationUrl = c.LocationUrl,
                Category = c.Category.Name,
                DealerId = c.Dealer.Id,
                DealerName = c.Dealer.Name,
                UserId = c.Dealer.UserId
            })
            .FirstOrDefault();

        public bool Edit(int id, string model, string description, string imageURL, decimal? pricePerHour, string locationUrl, int? categoryId, int dealerId)
        {
            var chargerData = this.data.Chargers.Find(id);

            if (chargerData.DealerId != dealerId)
            {
                return false;
            }

            chargerData.Model = model;
            chargerData.Description = description;
            chargerData.ImageURL = imageURL;
            chargerData.PricePerHour = pricePerHour;
            chargerData.LocationUrl = locationUrl;
            chargerData.CategoryId = categoryId;

            this.data.SaveChanges();

            return true;
        }

        private IEnumerable<ChargerServiceModel> GetChargers(IQueryable<Charger> chargerQuery)
            => chargerQuery
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
    }
}

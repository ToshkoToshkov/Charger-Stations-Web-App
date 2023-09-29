namespace Charger_Stations_Web_App.Controllers
{
    using Charger_Stations_Web_App.Data.Models;
    using Charger_Stations_Web_App.Data;
    using Microsoft.AspNetCore.Mvc;
    using Charger_Stations_Web_App.Models.Chargers;

    public class ChargersController : Controller
    {
        private readonly ChargerStationsDbContext data;

        public ChargersController(ChargerStationsDbContext data)
            => this.data = data;

        public IActionResult All([FromQuery]AllChargersQueryModel query)
        {
            var chargersQuery = this.data.Chargers.AsQueryable();

            if (!string.IsNullOrEmpty(query.Category))
            {
                chargersQuery = chargersQuery.Where(c => c.Category.Name == query.Category);
            }

            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                chargersQuery = chargersQuery.Where(c =>
                    c.Model.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    c.Description.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    c.Category.Name.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            chargersQuery = query.Sorting switch
            {
                AllChargersSorting.Model => chargersQuery.OrderByDescending(c => c.Id),
                _ => chargersQuery.OrderByDescending(c => c.Id)
            };

            var totalChargers = chargersQuery.Count();

            var chargers = chargersQuery
                .Skip((query.CurrentPage - 1) * AllChargersQueryModel.ChargersPerPage)
                .Take(AllChargersQueryModel.ChargersPerPage)
                .Select(c => new ChargerListingViewModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    ImageURL = c.ImageURL,
                    PricePerHour = c.PricePerHour,
                    //Owner = c.Owner,
                    LocationUrl = c.LocationUrl,
                    Category = c.Category.Name
                })
                .ToList();

            var chargerCategories = this.data
                .Chargers
                .Select(c => c.Category.Name)
                .Distinct()
                .ToList();

            query.TotalChargers = totalChargers;
            query.Categories = chargerCategories;
            query.Chargers = chargers;

            return View(query);
        }

        [HttpGet]
        public IActionResult Add() => View(new AddChargerFormModel
        {
            Categories = this.GetCategories()
        });

        [HttpPost]
        public IActionResult Add(AddChargerFormModel charger)
        {
            if (!this.data.Categories.Any(c => c.Id == charger.CategoryId))
            {
                this.ModelState.AddModelError(nameof(charger.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                charger.Categories = this.GetCategories();

                return View(charger);
            }

            var chargerData = new Charger
            {
                Model = charger.Model,
                Description = charger.Description,
                ImageURL = charger.ImageURL,
                PricePerHour = charger.PricePerHour,
                //Owner = charger.Owner,
                LocationUrl = charger.LocationUrl,
                CategoryId = charger.CategoryId
            };

            this.data.Chargers.Add(chargerData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private IEnumerable<ChargerCategoryViewModel> GetCategories()
           => this.data
            .Categories
            .Select(c => new ChargerCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToList();
               
    }
}

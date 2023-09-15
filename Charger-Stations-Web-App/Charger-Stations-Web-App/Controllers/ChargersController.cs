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
                Owner = charger.Owner,
                LocationUrl = charger.LocationUrl,
                CategoryId = charger.CategoryId
            };

            this.data.Chargers.Add(chargerData);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
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

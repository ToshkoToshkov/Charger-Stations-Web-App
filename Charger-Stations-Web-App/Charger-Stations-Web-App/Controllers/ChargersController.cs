namespace Charger_Stations_Web_App.Controllers
{
    using Charger_Stations_Web_App.Data;
    using Charger_Stations_Web_App.Data.Models;
    using Charger_Stations_Web_App.Models.Chargers;
    using Charger_Stations_Web_App.Services.Chargers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static Charger_Stations_Web_App.Infrastructure.ClaimsPrincipalExtensions;

    public class ChargersController : Controller
    {
        private readonly IChargersService chargers;
        private readonly ChargerStationsDbContext data;

        public ChargersController(IChargersService chargers, ChargerStationsDbContext data)
        {
            this.chargers = chargers;
            this.data = data;
        }

        public IActionResult All([FromQuery]AllChargersQueryModel query)
        {
            var queryResult = this.chargers.All(query.Model, query.SearchTerm, query.Sorting, query.CurrentPage, AllChargersQueryModel.ChargersPerPage);

            var chargerCategories = this.chargers.AllChargerCategories();

            query.TotalChargers = queryResult.TotalChargers;
            query.Categories = chargerCategories;
            query.Chargers = queryResult.Chargers;

            return View(query);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsDealer())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            return View(new AddChargerFormModel
            {
                Categories = this.GetCategories()
            });
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myChargers = this.chargers.ByUser(this.User.GetId());
            return View(myChargers);
        }


        [HttpPost]
        [Authorize]
        public IActionResult Add(AddChargerFormModel charger)
        {
            var dealerId = this.data
                .Dealers
                .Where(d => d.UserId == this.User.GetId())
                .Select(d => d.Id)
                .FirstOrDefault();

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

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
                LocationUrl = charger.LocationUrl,
                CategoryId = charger.CategoryId,
                DealerId = dealerId
            };

            this.data.Chargers.Add(chargerData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private bool UserIsDealer()
            => this.data
            .Dealers
            .Any(d => d.UserId == this.User.GetId());

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

namespace Charger_Stations_Web_App.Controllers
{
    using Charger_Stations_Web_App.Data;
    using Charger_Stations_Web_App.Models.Chargers;
    using Charger_Stations_Web_App.Services.Chargers;
    using Charger_Stations_Web_App.Services.Dealers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static Charger_Stations_Web_App.Infrastructure.ClaimsPrincipalExtensions;

    public class ChargersController : Controller
    {
        private readonly IChargersService chargers;
        private readonly IDealerService dealers;
        private readonly ChargerStationsDbContext data;

        public ChargersController(IChargersService chargers, IDealerService dealers, ChargerStationsDbContext data)
        {
            this.chargers = chargers;
            this.dealers = dealers;
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
            if (!this.dealers.IsDealer(this.User.GetId()))
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            return View(new ChargerFormModel
            {
                Categories = this.GetCategories()
            });
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.dealers.IsDealer(userId))
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            var charger = this.chargers.Details(id);

            if (charger.UserId != userId)
            {
                return Unauthorized();
            }

            return View(new ChargerFormModel
            {
                Model = charger.Model,
                Description = charger.Description,
                ImageURL = charger.ImageURL,
                PricePerHour = charger.PricePerHour,
                CategoryId = charger.CategoryId,
                LocationUrl = charger.LocationUrl,
                Categories = this.GetCategories(),
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
        public IActionResult Add(ChargerFormModel charger)
        {
            var dealerId = this.dealers.GetIdByUser(this.User.GetId());

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!this.chargers.CategoryExist(charger.CategoryId))
            {
                this.ModelState.AddModelError(nameof(charger.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                charger.Categories = this.GetCategories();

                return View(charger);
            }

            this.chargers.Create(
                charger.Model,
                charger.Description,
                charger.ImageURL,
                charger.PricePerHour,
                charger.LocationUrl,
                charger.CategoryId,
                dealerId);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, ChargerFormModel charger)
        {
            var dealerId = this.dealers.GetIdByUser(this.User.GetId());

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!this.chargers.CategoryExist(charger.CategoryId))
            {
                this.ModelState.AddModelError(nameof(charger.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                charger.Categories = this.GetCategories();

                return View(charger);
            }

            var chargerIsEdited = this.chargers.Edit(
                id,
                charger.Model,
                charger.Description,
                charger.ImageURL,
                charger.PricePerHour,
                charger.LocationUrl,
                charger.CategoryId,
                dealerId);

            if (!chargerIsEdited)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var userId = this.User.GetId();

            if (!this.dealers.IsDealer(userId))
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            var charger = this.chargers.Details(id);

            if (charger.UserId != userId)
            {
                return Unauthorized();
            }

            var chargerIsDeleted = this.chargers.Delete(charger.Id);

            if (!chargerIsDeleted)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Mine));
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

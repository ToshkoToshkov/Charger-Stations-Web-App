namespace Charger_Stations_Web_App.Controllers
{
    using Charger_Stations_Web_App.Data;
    using Charger_Stations_Web_App.Data.Models;
    using Charger_Stations_Web_App.Infrastructure;
    using Charger_Stations_Web_App.Models.Dealers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DealersController : Controller
    {
        private readonly ChargerStationsDbContext data;

        public DealersController(ChargerStationsDbContext data) 
            => this.data = data;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeDealerFormModel dealer)
        {
            var userId = this.User.GetId();
            var userIsAlreadyDealer = this.data
                .Dealers
                .Any(d => d.UserId == userId);

            if (userIsAlreadyDealer)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(dealer);
            }

            var dealerData = new Dealer
            {
                Name = dealer.Name,
                PhoneNumber = dealer.PhoneNumber,
                UserId = userId
            };

            this.data.Dealers.Add(dealerData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Chargers");
        }
    }
}

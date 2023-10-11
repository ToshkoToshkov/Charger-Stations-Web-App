namespace Charger_Stations_Web_App.Controllers.Api
{
    using Charger_Stations_Web_App.Models.Api.Chargers;
    using Charger_Stations_Web_App.Services.Chargers;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/chargers")]
    public class ChargersApiController : ControllerBase
    {
        private readonly IChargersService chargers;

        public ChargersApiController(IChargersService chargers)
            => this.chargers = chargers;

        [HttpGet]
        public ChargersQueryServiceModel All([FromQuery] AllChargersApiRequestModel query)
            => this.chargers.All(query.Model, query.SearchTerm, query.Sorting, query.CurrentPage, query.ChargersPerPage); 
            //var chargerCategories = this.data
            //    .Chargers
            //    .Select(c => c.Category.Name)
            //    .Distinct()
            //    .ToList();
    }
}

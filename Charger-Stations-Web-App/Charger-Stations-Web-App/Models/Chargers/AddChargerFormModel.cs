namespace Charger_Stations_Web_App.Models.Chargers
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class AddChargerFormModel
    {
        [Required]
        [StringLength(ChargerModelMaxLength, MinimumLength = ChargerModelMinLength,
            ErrorMessage = "The model name must be between {2} and {1} symbols")]
        public string Model { get; init; }

        [Required]
        [StringLength(ChargerDescriptionMaxLength, MinimumLength = ChargerDescriptionMinLength,
            ErrorMessage = "The description must be between {2} and {1} symbols")]
        public string Description { get; init; }

        [Display(Name = "Image URL")]
        [Required]
        public string ImageURL { get; init; }

        [Display(Name = "Price for 1 hour charging")]
        [Range(ChargerPriceMinValue, ChargerPriceMaxValue)]
        public decimal? PricePerHour { get; init; }

        [Required]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength,
            ErrorMessage = "The owner name must be between {2} and {1} symbols")]
        public string Owner { get; init; }

        [Display(Name = "Category")]
        public int? CategoryId { get; init; }

        [Display(Name = "Location URL")]
        [Required]
        public string LocationUrl { get; init; }

        public IEnumerable<ChargerCategoryViewModel>? Categories { get; set; }
    }
}

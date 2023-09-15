namespace Charger_Stations_Web_App.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class Charger
    {
        [Required]
        public int Id { get; init; }

        [Required]
        [MaxLength(ChargerModelMaxLength)]
        public string Model { get; set; }

        [Required]
        [MaxLength(ChargerDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ImageURL { get; set; }

        [Range(ChargerPriceMinValue, ChargerPriceMaxValue)]
        public decimal? PricePerHour { get; set; }

        public string? Owner { get; set; }

        public string? LocationUrl { get; set; }

        public int? CategoryId { get; set; }

        public Category? Category { get; init; }
    }
}

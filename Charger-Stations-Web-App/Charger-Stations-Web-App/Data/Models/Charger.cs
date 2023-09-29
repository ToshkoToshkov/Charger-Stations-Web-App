namespace Charger_Stations_Web_App.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Charger;

    public class Charger
    {
        [Required]
        public int Id { get; init; }

        [Required]
        [MaxLength(ModelMaxLength)]
        public string Model { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string ImageURL { get; set; }

        [Range(PriceMinValue, PriceMaxValue)]
        public decimal? PricePerHour { get; set; }

        //public string? Owner { get; set; }
        public string? LocationUrl { get; set; }

        public int? CategoryId { get; set; }

        public Category? Category { get; init; }

        public int? DealerId { get; init; }

        public Dealer Dealer { get; init; }
    }
}

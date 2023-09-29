namespace Charger_Stations_Web_App.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Category;

    public class Category
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Charger>? Chargers { get; init; } = new List<Charger>();
    }
}

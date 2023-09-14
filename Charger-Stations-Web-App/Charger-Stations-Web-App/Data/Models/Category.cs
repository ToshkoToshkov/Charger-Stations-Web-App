namespace Charger_Stations_Web_App.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Charger> Chargers { get; set; } = new List<Charger>();
    }
}

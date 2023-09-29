namespace Charger_Stations_Web_App.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Dealer;
    public class Dealer
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Charger> Chargers { get; init; } = new List<Charger>();
    }
}

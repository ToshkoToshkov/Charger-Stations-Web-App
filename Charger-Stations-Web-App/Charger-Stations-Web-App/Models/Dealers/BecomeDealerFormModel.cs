namespace Charger_Stations_Web_App.Models.Dealers
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Dealer;
    public class BecomeDealerFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "The name of the dealer must be between {2} and {1} symbols!")]
        public string Name { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength, ErrorMessage = "The phone of the dealer must be between {2} and {1} symbols!")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}

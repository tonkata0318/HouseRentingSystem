using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Data.DataConstraints;

namespace HouseRentingSystem.Models.Agent
{
    public class BecomeAgentFormModel
    {
        [Required]
        [StringLength(phonenumberMaxLength, MinimumLength = phoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
    }
}

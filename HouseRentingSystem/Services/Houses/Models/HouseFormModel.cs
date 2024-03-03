using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Data.DataConstraints;

namespace HouseRentingSystem.Services.Houses.Models
{
    public class HouseFormModel
    {
        [Required]
        [StringLength(HouseTitleMax, MinimumLength = HouseTitleMin)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(HouseAddressMaxLength, MinimumLength = HouseAddressMinLength)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(HouseDescriptionMaxLength, MinimumLength = HouseDescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Display(Name ="Image URL")]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [Range(0.00,housePricePerMonthMax,ErrorMessage ="Price per Month must be a positive number and less than {2} leva")]
        [Display(Name ="Price Per Month")]
        public decimal PricePerMonth { get; set; }

        [Display(Name ="Category")]
        public int CategoryId { get; set; }

        public IEnumerable<HouseCategoryServiceModel> Categories { get; set; }
        = new List<HouseCategoryServiceModel>();
    }
}

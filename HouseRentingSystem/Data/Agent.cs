using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HouseRentingSystem.Data.DataConstraints;

namespace HouseRentingSystem.Data
{
    public class Agent
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(phonenumberMaxLength)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;
    }
}
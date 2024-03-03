using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Data.DataConstraints;

namespace HouseRentingSystem.Data
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(categoryNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public IEnumerable<House> Houses { get; set; } = new List<House>();
    }
}

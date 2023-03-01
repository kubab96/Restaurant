using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class DishUpsertDTO
    {
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int RestaurantId { get; set; }
    }
}

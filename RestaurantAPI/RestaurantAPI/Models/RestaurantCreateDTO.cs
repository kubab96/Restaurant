using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class RestaurantCreateDTO
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public bool HasDelivery { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(50)]
        public string Street { get; set; }
        public string? PostalCode { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Nazwa może zawierać tylko litery i cyfry.")]
        public string Name { get; set; }

        [Required]
        [Range(0, 50000)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public string Category { get; set; }
    }
}

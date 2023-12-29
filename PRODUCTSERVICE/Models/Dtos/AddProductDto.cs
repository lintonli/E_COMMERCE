using System.ComponentModel.DataAnnotations;

namespace PRODUCTSERVICE.Models.Dtos
{
    public class AddProductDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Image { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
    }
}

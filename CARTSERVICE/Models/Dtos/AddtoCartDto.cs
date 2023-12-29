using System.ComponentModel.DataAnnotations;

namespace CARTSERVICE.Models.Dtos
{
    public class AddtoCartDto
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int ProductQuantity { get; set; }
    }
}


using System.ComponentModel.DataAnnotations.Schema;

namespace ORDERSERVICE.Models.Dtos
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<CartItemsDto> CartItems { get; set; } = new List<CartItemsDto>();

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }
    }
}

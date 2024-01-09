using System.ComponentModel.DataAnnotations.Schema;

namespace ORDERSERVICE.Models.Dtos
{
    public class CartItemsDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
    }
}

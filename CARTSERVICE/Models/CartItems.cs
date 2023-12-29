using System.ComponentModel.DataAnnotations.Schema;

namespace CARTSERVICE.Models
{
    public class CartItems
    {

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }

        [ForeignKey("Cart")]
        public Guid CartId { get; set; }
        public Cart Cart { get; set; } = default!;
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ORDERSERVICE.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public double Discount { get; set; }
        public string? StripeSessionId { get; set; }

        public string Status { get; set; } = "Pending";
        public Guid CartId { get; set; }

        public string PaymentIntent { get; set; } = string.Empty;
    }
}

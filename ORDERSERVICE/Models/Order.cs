using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ORDERSERVICE.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public int UserId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }
    }
}

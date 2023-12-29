using System.ComponentModel.DataAnnotations;

namespace COUPON.Models.Dtos
{
    public class AddCouponDto
    {
        [Required]
        public string CouponCode { get; set; } = string.Empty;
        [Required]
        [Range(100, 10000)]
        public int CouponAmount { get; set; }
        [Required]
        [Range(500, int.MaxValue)]
        public int CouponMinAmount { get; set; }
    }
}

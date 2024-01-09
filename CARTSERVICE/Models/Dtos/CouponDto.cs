namespace CARTSERVICE.Models.Dtos
{
    public class CouponDto
    {
        public string CouponCode { get; set; } = string.Empty;
        public int CouponAmount { get; set; }
        public int CouponMinAmount { get; set; }
    }
}

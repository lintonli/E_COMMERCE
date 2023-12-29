namespace COUPON.Models
{
    public class Coupon
    {
        public Guid Id {  get; set; }
        public string CouponCode {  get; set; }=string.Empty;
        public int  CouponAmount { get; set; }
        public int CouponMinAmount {  get; set; }
    }
}

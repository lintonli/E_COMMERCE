namespace ORDERSERVICE.Models.Dtos
{
    public class RewardsDto
    {
        public Guid OrderId { get; set;}
        public decimal TotalAmount {  get; set;}
        public string Name {  get; set;}= string.Empty;
        public string Email {  get; set;}=String.Empty;
    }
}

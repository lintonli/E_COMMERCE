namespace CARTSERVICE.Models.Dtos
{
    public class CartResponseDto
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public List<CartItemsDto> CartItems { get; set; } = new List<CartItemsDto>();
        public decimal TotalAmount { get; set; }
    }
}

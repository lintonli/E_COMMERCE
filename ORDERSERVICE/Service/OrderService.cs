using ORDERSERVICE.Data;
using Microsoft.EntityFrameworkCore;
using ORDERSERVICE.Models;
using ORDERSERVICE.Service.Iservice;
using Stripe.Climate;
using ORDERSERVICE.Models.Dtos;
using Stripe.Checkout;
using Stripe;
using EcommMessageBus;

namespace ORDERSERVICE.Service
{
    public class OrderService : IOrder
    {
        private readonly ApplicationDbContext _context;
        private readonly ICart _cartService;
        private readonly IUser _userService;
        private readonly IMessageBus _messageBus;
        
        public OrderService(ApplicationDbContext context, ICart cart, IUser user, IMessageBus messageBus)
        {
            _context = context;
            _cartService = cart;
            _userService = user;
            _messageBus = messageBus;
        }
        public async Task<string> AddOrder(Models.Order order)
        {
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return "Order added Successfully";
            }
            catch (Exception ex)
            {
                return $"{ex.InnerException.Message}";
            }
        }

        

        public async Task<string> DeleteOrder(Models.Order ord)
        {
            _context.Orders.Remove(ord);
            await _context.SaveChangesAsync();
            return "Order deleted successfully";
        }

      

        public async Task<Models.Order> GetOrderById(Guid Id)
        {
            return await _context.Orders.Where(x => x.Id== Id).FirstOrDefaultAsync();
        }

        public async Task<List<Models.Order>> GetOrderByUserId(Guid UserId)
        {
           var order = await _context.Orders.Where(x => x. Id==UserId).FirstOrDefaultAsync();
            if (order == null)
            {
                return new List<Models.Order>();
            }
            return null;
        }

        public async  Task<List<Models.Order>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        

        public async Task<StripeRequestDto> MakePayments(StripeRequestDto stripeRequestDto)
        {
            var order = await _context.Orders.Where(x => x.Id == stripeRequestDto.OrderId).FirstOrDefaultAsync();
            var cart = await _cartService.GetCartById(order.CartId);

            var options = new SessionCreateOptions()
            {
                SuccessUrl = stripeRequestDto.ApprovedUrl,
                CancelUrl = stripeRequestDto.CancelUrl,
                Mode = "payment",
                LineItems = new List<SessionLineItemOptions>()
            };

            foreach (var cartitem in cart.CartItems)
            {
                var items = new SessionLineItemOptions()
                {
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        UnitAmount = (long)order.TotalAmount * 100,
                        Currency = "kes",

                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name= cartitem.ProductName,
                            Description= cartitem.ProductDescription,
                            Images= new List<string> { cartitem.ProductImage }
                        }
                       
                    },
                    Quantity =cartitem.ProductQuantity
                };
                options.LineItems.Add(items);
            }
            var DiscountObject = new List<SessionDiscountOptions>()
            {
                new SessionDiscountOptions()
                {
                    Coupon=order.CouponCode
                }
            };

            if (order.Discount > 0)
            {
                options.Discounts = DiscountObject;

            }

            var service = new SessionService();
            Session session= service.Create(options);

            stripeRequestDto.StripeSessionUrl= session.Url;
            stripeRequestDto.StripeSessionId = session.Id;

            order.StripeSessionId = session.Id;
            order.Status = "Ongoing";
            await _context.SaveChangesAsync();
            return stripeRequestDto;
        }

        public async Task saveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<string> UpdateOrder(Models.Order order)
        {
            await _context.SaveChangesAsync();
            return "Order Successfully updated";
        }

        public async Task<bool> ValidatePayments(Guid OrderId)
        {

            var order = await _context.Orders.Where(x => x.Id == OrderId).FirstOrDefaultAsync();

            var service = new SessionService();
            Session session = service.Get(order.StripeSessionId);

            PaymentIntentService paymentIntentService = new PaymentIntentService();

            PaymentIntent paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

            if (paymentIntent.Status == "succeeded")
            {
                //the payment was success

                order.Status = "Paid";
                order.PaymentIntent = paymentIntent.Id;
                await _context.SaveChangesAsync();

                var user = await _userService.GetUserById(order.UserId);

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    return false;
                }
                else
                {
                    var reward = new RewardsDto()
                    {
                        OrderId = order.Id,
                        TotalAmount = order.TotalAmount,
                        Name = user.Name,
                        Email = user.Email

                    };
                    await _messageBus.PublishMessage(reward, "orderadded");
                }

                // Send an Email to User
                //Reward the user with some Bonus Points 
                return true;

            }
            return false;
        }
    }
}
  

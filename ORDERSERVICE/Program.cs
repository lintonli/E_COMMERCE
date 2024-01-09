using ORDERSERVICE.Service.Iservice;
using Microsoft.EntityFrameworkCore;
using ORDERSERVICE.Data;
using ORDERSERVICE.Extensions;
using ORDERSERVICE.Service;
using EcommMessageBus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddAuth();
builder.AddSwaggenGenExtension();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection"));
});

builder.Services.AddScoped<ICart, CartService>();
builder.Services.AddScoped<IOrder, OrderService>();
builder.Services.AddScoped<ICoupon, CouponService>();   
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IMessageBus, MessageBus>();



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpClient("Cart", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURL:CartServiceURL")));
builder.Services.AddHttpClient("User", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURL:UserServiceURL")));
builder.Services.AddHttpClient("Coupon", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURL:CouponServiceURL")));
Stripe.StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("Stripe:Key");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMigrations();

app.UseAuthorization();

app.MapControllers();

app.Run();

using CARTSERVICE.Data;
using CARTSERVICE.Extensions;
using CARTSERVICE.Services;
using CARTSERVICE.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient("Product", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURL:ProductServiceURL")));
builder.Services.AddHttpClient("Coupon", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURL:CouponServiceURL")));
builder.Services.AddScoped<IProduct, ProductService>();
builder.Services.AddScoped<ICart, CartServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMigrations();

app.UseMigrations();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

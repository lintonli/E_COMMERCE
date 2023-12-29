using CARTSERVICE.Models;
using Microsoft.EntityFrameworkCore;

namespace CARTSERVICE.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options) { }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItems> CartItems { get; set; }
    }
}

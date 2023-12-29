using Microsoft.EntityFrameworkCore;
using ORDERSERVICE.Models;

namespace ORDERSERVICE.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }
       public DbSet<Order> Orders { get; set; }
    }
}

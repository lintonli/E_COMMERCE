using Microsoft.EntityFrameworkCore;
using PRODUCTSERVICE.Models;

namespace PRODUCTSERVICE.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Product>Products { get; set; }
    }
}

using COUPON.Models;
using Microsoft.EntityFrameworkCore;

namespace COUPON.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) { }
        public DbSet<Coupon>Coupons { get; set; }   
    }
}

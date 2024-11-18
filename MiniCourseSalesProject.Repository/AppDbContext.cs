using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniCourseSalesProject.Repository.Entities;
using System.Reflection.Emit;

namespace MiniCourseSalesProject.Repository
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser, AppRole, Guid>(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // User -> Orders (One-to-Many)
            builder.Entity<AppUser>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            // Course -> Category (Many-to-One)
            builder.Entity<Course>()
                .HasOne(c => c.Category)
                .WithMany(cat => cat.Courses)
                .HasForeignKey(c => c.CategoryId);

            // Order -> Courses (Many-to-Many)
            builder.Entity<Order>()
                .HasMany(o => o.Courses)
                .WithMany(c => c.Orders)
                .UsingEntity(j => j.ToTable("OrdersCourses"));

            // Order -> Payment (One-to-One)
            builder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderId);

            builder.Entity<Payment>().Property(p => p.Amount).HasPrecision(18, 2);
            builder.Entity<Course>().Property(p => p.Price).HasPrecision(18, 2);
            builder.Entity<Order>().Property(p => p.TotalAmount).HasPrecision(18, 2);
            builder.Entity<AppUser>().Property(p => p.Wallet).HasPrecision(18, 2);

            base.OnModelCreating(builder);
        }
    }
}

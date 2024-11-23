using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniCourseSalesProject.Repository.Entities;

namespace MiniCourseSalesProject.Repository
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser, AppRole, Guid>(options)
    {
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }

        // OnModelCreating metodu, ilişki yapılarını daha da özelleştirebiliriz
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            // BasketItem ve Course arasındaki ilişkiyi belirtme
            modelBuilder.Entity<BasketItem>()
                .HasOne(bi => bi.Course)
                .WithMany(c => c.BasketItems)
                .HasForeignKey(bi => bi.CourseId);

            // Basket ve BasketItem arasındaki ilişkiyi belirtme
            modelBuilder.Entity<BasketItem>()
                .HasOne(bi => bi.Basket)
                .WithMany(b => b.BasketItems)
                .HasForeignKey(bi => bi.BasketId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order ve Basket arasındaki ilişkiyi belirtme
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Basket)
                .WithOne(b => b.Order)
                .HasForeignKey<Order>(o => o.BasketId)
                .OnDelete(DeleteBehavior.SetNull);

            // Order ve User arasındaki ilişkiyi belirtme
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            // Payment ve Order arasındaki ilişkiyi belirtme
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderId);

            // Course ve Category arasındaki ilişkiyi belirtme
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Courses)
                .HasForeignKey(c => c.CategoryId);

            // Basket ve User arasındaki ilişkiyi belirtme
            modelBuilder.Entity<Basket>()
                .HasOne(b => b.User)
                .WithMany(u => u.Baskets)
                .HasForeignKey(b => b.UserId);


            modelBuilder.Entity<AppUser>()
                .Property(u => u.Wallet)
                .HasPrecision(18, 2); // Toplam 18 basamak, 2'si ondalık basamak

            modelBuilder.Entity<Course>()
                .Property(c => c.Price)
                .HasPrecision(18, 2); // Toplam 18 basamak, 2'si ondalık basamak

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2); // Toplam 18 basamak, 2'si ondalık basamak

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2); // Toplam 18 basamak, 2'si ondalık basamak

            // Diğer özelleştirilmiş yapılandırmalar
            base.OnModelCreating(modelBuilder);
        }
    }
}

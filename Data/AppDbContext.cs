using Microsoft.EntityFrameworkCore;
using MyFood.Models;

namespace MyFood.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<MealItem> MealItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Meal>()
                .HasMany(m => m.MealItems)
                .WithOne(mi => mi.Meal)
                .HasForeignKey(mi => mi.MealId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MealItem>()
                .HasOne(mi => mi.Food)
                .WithMany()
                .HasForeignKey(mi => mi.FoodId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

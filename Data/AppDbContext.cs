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
        public DbSet<MealFood> MealFoods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Food>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MealFood>()
                .HasOne<Meal>()
                .WithMany()
                .HasForeignKey(mf => mf.MealId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MealFood>()
                .HasOne<Food>()
                .WithMany()
                .HasForeignKey(mf => mf.FoodId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Meal>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NutritionalGoal>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<NutritionalGoal>(ng => ng.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Entities
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Dish>(d =>
            {
                d.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<User>(u =>
            {
                u.Property(e => e.Email).IsRequired();
            });

            modelBuilder.Entity<Role>(u =>
            {
                u.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Address>(a =>
            {
                a.Property(e => e.City).IsRequired().HasMaxLength(50);
                a.Property(e => e.Street).IsRequired().HasMaxLength(50);
            });
        }
    }

    
}

using Microsoft.EntityFrameworkCore;
using RestaurantBooking.Data.Entities;

namespace RestaurantBooking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<TableClaim> TableClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Navigation(e => e.Roles).AutoInclude();
            });

            SeedRoles(modelBuilder);
            SeedUsers(modelBuilder);
            SeedRoleUser(modelBuilder);
            SeedRestaurants(modelBuilder);
            SeedTables(modelBuilder);
        }

        private void SeedUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new[]
            {
                new User{Id = 1, Email = "owner1@test.com", Name = "Owner 1", PasswordHash = "$2a$11$msZrWJt7bp5ezODcspIcVeSv8sVWBFw4nh8XwKRlIGu8LlwvBdtCy", Phone = "123"},
                new User{Id = 2, Email = "owner2@test.com", Name = "Owner 2", PasswordHash = "$2a$11$msZrWJt7bp5ezODcspIcVeSv8sVWBFw4nh8XwKRlIGu8LlwvBdtCy", Phone = "123"},
                new User{Id = 3, Email = "visitor@test.com", Name = "Visitor 1", PasswordHash = "$2a$11$msZrWJt7bp5ezODcspIcVeSv8sVWBFw4nh8XwKRlIGu8LlwvBdtCy", Phone = "123"},
            });
        }

        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Member" });
        }

        private void SeedRoleUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity("RoleUser").HasData(new[]
            {
                new { UsersId = 1, RolesId = 1 },
                new { UsersId = 2, RolesId = 1 },
                new { UsersId = 3, RolesId = 2 },
            });
        }

        private void SeedRestaurants(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>().HasData(new[]
            {
                new Restaurant { Id = 1, OwnerUserId = 1, Name = "Test rest", Description = "Owner is owner 1", Address = "Test address", PhoneNumber = "+79991112233", TablesCount = 3 },
                new Restaurant { Id = 2, OwnerUserId = 2, Name = "Test rest2", Description = "Owner is owner 2", Address = "Test address2", PhoneNumber = "+79991112233", TablesCount = 3 }
            });
        }

        private void SeedTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table>().HasData(new[]
            {
                new Table { Id = 1, RestaurantId = 1, TableNumber = 1,},
                new Table { Id = 2, RestaurantId = 1, TableNumber = 2 },
                new Table { Id = 3, RestaurantId = 1, TableNumber = 3 },
                new Table { Id = 4, RestaurantId = 2, TableNumber = 1,},
                new Table { Id = 5, RestaurantId = 2, TableNumber = 2 },
                new Table { Id = 6, RestaurantId = 2, TableNumber = 3 },
            });
        }
    }
}
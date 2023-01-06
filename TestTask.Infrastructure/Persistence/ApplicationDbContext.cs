using Microsoft.EntityFrameworkCore;
using TestTask.Application.Abstractions;
using TestTask.Application.Common;
using TestTask.Domain.Entities;
using TestTask.Infrastructure.Persistence.Configurations;

namespace TestTask.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            AddConfigurations(builder);
            SeedData(builder);
        }

        private void AddConfigurations(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserEntityTypeConfiguration());
            builder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        }


        private void SeedData(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(
                new[]
                {
                    new User{ Id = Guid.NewGuid(),  UserName = "Test1", PasswordHash = "U+CMicHhIuvh3W73vtQ1LE9SIr0WweXjdnfHam8if+g=", Role = RoleConstants.AdminRoleName }, 
                    new User{ Id = Guid.NewGuid(), UserName = "Test2", PasswordHash = "U+CMicHhIuvh3W73vtQ1LE9SIr0WweXjdnfHam8if+g=", Role = RoleConstants.UserRoleName }
                });

            builder.Entity<Product>().HasData(
                new[]
                {
                    new Product{ Id = Guid.NewGuid(), Name = "HDD 1TB", Quantity = 55, Price = 74.09 },
                    new Product{ Id = Guid.NewGuid(), Name = "HDD SSD 512GB", Quantity = 102, Price = 190.99 },
                    new Product{ Id = Guid.NewGuid(), Name = "RAM DDR4 16GB", Quantity = 47, Price = 80.32 },
                });
        }
    }
}

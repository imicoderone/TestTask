using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Domain.Entities;

namespace TestTask.Infrastructure.Persistence.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserName)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(p => p.UserName)
                .IsUnique();
        }
    }
}

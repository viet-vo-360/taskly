using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskly.Users.Models;

namespace Taskly.Dashboard.App.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.PhoneNumber)
                   .IsUnique();

            builder.Property(e => e.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.HasIndex(e => e.Email)
                   .IsUnique();

            builder.Property(e => e.Email)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(e => e.CreatedAt)
                   .HasPrecision(0)
                   .HasDefaultValueSql("(GETUTCDATE())");

            builder.Property(e => e.Dob)
                   .IsRequired()
                   .HasColumnName("DOB");

            builder.Property(e => e.FirstName)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(e => e.IsActive)
                   .HasDefaultValue(true);

            builder.Property(e => e.LastName)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(e => e.MiddleName)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(e => e.Password)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(e => e.UpdatedAt)
                   .HasPrecision(0)
                   .HasDefaultValueSql("(GETUTCDATE())");
        }
    }
}
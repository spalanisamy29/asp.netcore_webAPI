using LoanManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("Users");

            entity.HasKey(e => e.UserId);

            entity.Property(e => e.FirstName)
                  .HasMaxLength(20)
                  .IsRequired();

            entity.Property(e => e.LastName)
                  .HasMaxLength(20)
                  .IsRequired();

            entity.Property(e => e.Email)
                  .HasMaxLength(50)
                  .IsRequired();

            entity.HasIndex(e => e.Email)
                  .IsUnique();

            entity.Property(e => e.PasswordHash)
                  .HasMaxLength(150)
                  .IsRequired();

            entity.Property(e => e.PhoneNumber)
                  .HasMaxLength(20);

            entity.Property(e => e.NativePlace)
                  .HasMaxLength(150);

            entity.Property(e => e.IsActive)
                  .HasDefaultValue(true);

            entity.Property(e => e.CreatedDate)
                  .HasColumnType("datetime");

            entity.Property(e => e.UpdatedDate)
                  .HasColumnType("datetime");
        }
    }
}

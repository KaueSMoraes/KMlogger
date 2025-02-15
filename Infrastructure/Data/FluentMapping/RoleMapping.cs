using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.FluentMapping;

internal class RoleMapping : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        builder.HasKey(u => u.Id).HasName("PK_Roles");
        
        //Properties
        builder.Property(c => c.Id)
            .HasColumnName("Id")
            .HasColumnType("uuid")
            .IsRequired().ValueGeneratedOnAdd();

        builder.Property(c => c.CreatedDate)
            .HasColumnName("CreatedDate")
            .HasColumnType("timestamp")
            .IsRequired();

        builder.Property(c => c.UpdatedDate)
            .HasColumnName("UpdatedDate")
            .HasColumnType("timestamp")
            .IsRequired();

        builder.Property(c => c.DeletedDate)
            .HasColumnName("DeletedDate")
            .HasColumnType("timestamp").IsRequired(false);
        
        builder.OwnsOne(u => u.Name, name =>
        {
            name.Property(f => f.Name).HasMaxLength(100);
        });

        builder.Property(u => u.Slug).HasColumnName("Slug").IsRequired();
        builder
            .HasMany(u => u.Users)
            .WithMany(u => u.Roles)
            .UsingEntity<Dictionary<string, object>>(
                "UserRole",
                user => user.HasOne<User>().WithMany().HasForeignKey("UserId"),
                role => role.HasOne<Role>().WithMany().HasForeignKey("RoleId"));
    }
}
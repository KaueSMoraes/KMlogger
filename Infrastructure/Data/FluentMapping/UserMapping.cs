using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.FluentMapping;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id).HasName("PK_Users");

        // Properties
        builder.Property(c => c.Id)
            .HasColumnName("Id")
            .HasColumnType("uuid")
            .IsRequired().ValueGeneratedOnAdd();

        builder.Property(c => c.CreatedDate)
            .HasColumnName("CreatedDate")
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(c => c.UpdatedDate)
            .HasColumnName("UpdatedDate")
            .HasColumnType("timestamp")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP") 
            .ValueGeneratedOnAddOrUpdate();

        builder.Property(c => c.DeletedDate)
            .HasColumnName("DeletedDate")
            .HasColumnType("timestamp").IsRequired(false);

        builder.OwnsOne(u => u.FullName, fullName =>
        {
            fullName.Property(f => f.FirstName).HasMaxLength(100).HasColumnName("FirstName");
            fullName.Property(f => f.LastName).HasMaxLength(100).HasColumnName("LastName");
        });

        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Address).HasColumnName("Email").HasMaxLength(50);
        });

        builder.OwnsOne(u => u.Address, address =>
        {
            address.Property(a => a.Road).HasColumnName("Road").HasMaxLength(100);
            address.Property(a => a.Number).HasColumnName("Number").HasColumnType("BIGINT");
            address.Property(a => a.NeighBordHood).HasColumnName("NeighborHood").HasColumnType("varchar");
            address.Property(a => a.Complement).HasColumnName("Complement").HasColumnType("varchar").HasMaxLength(100);
        });


        builder.Property(u => u.TokenActivate).HasColumnName("TokenActivate");
        builder.Property(u => u.Active).HasColumnName("Active");
        builder.OwnsOne(u => u.Password, password =>
        {
            password.Property(p => p.Hash).HasColumnName("Hash").IsRequired();
            password.Property(p => p.Salt).HasColumnName("Salt").IsRequired();
            password.Ignore(p => p.Content); 
        });
        
        builder
            .HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRole",
                role => role
                    .HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .HasConstraintName("FK_UserRole_RoleId")
                    .OnDelete(DeleteBehavior.Cascade),
                user => user
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_UserRole_UserId")
                    .OnDelete(DeleteBehavior.Cascade));
    }
}

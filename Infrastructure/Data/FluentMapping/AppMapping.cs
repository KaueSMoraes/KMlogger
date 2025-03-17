using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.FluentMapping;

public  class AppMapping : IEntityTypeConfiguration<App>
{
    public void Configure(EntityTypeBuilder<App> builder)
    {
        builder.ToTable("App");
        builder.HasKey(x => x.Id).HasName("PK_App");

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

        builder.Property(x => x.Active).IsRequired();
        builder.Property(x => x.Environment).IsRequired(false);
        
        builder.OwnsOne(x => x.Name, nameBuilder =>
        {
            nameBuilder.Property(n => n.Name)
                .HasColumnName("Name")
                .IsRequired();
        });

        builder.HasOne(x => x.Category).WithMany().HasForeignKey("CategoryId");
        builder.HasMany(x => x.Logs).WithOne().HasForeignKey("AppId");
    }
}

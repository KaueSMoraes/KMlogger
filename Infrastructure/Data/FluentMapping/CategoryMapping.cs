using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.FluentMapping;

public  class CategoryMapping : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Nome da tabela
        builder.ToTable("Categories");
        // Configuração da chave primária
        builder.HasKey(c => c.Id).HasName("PK_Categories");

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


        // Propriedade Name (ValueObject)
        builder.OwnsOne(c => c.Name, nameBuilder =>
        {
            nameBuilder.Property(n => n.Name)
                .HasColumnName("Name")
                .IsRequired();
        });

        // Outras Propriedades
        builder.Property(c => c.Active)
            .HasColumnName("Active")
            .IsRequired();

        builder.HasMany(c => c.Apps).WithOne().HasForeignKey("CategoryId");
    }
}

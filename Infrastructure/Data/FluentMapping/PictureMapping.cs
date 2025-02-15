using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.FluentMapping;

internal class PictureMapping : IEntityTypeConfiguration<Picture>
{
    public void Configure(EntityTypeBuilder<Picture> builder)
    {
        // Configuração da chave primária
        builder.HasKey(p => p.Id);

        builder.OwnsOne(p => p.File, fileBuilder =>
        {
            fileBuilder.Property(f => f.FileName)
                .HasColumnName("FileName")
                .IsRequired();

            fileBuilder.Property(f => f.FileSize)
                .HasColumnName("FileSize");

            fileBuilder.Ignore(f => f.File);
        });

        // Propriedade Name (ValueObject)
        builder.OwnsOne(p => p.Name, nameBuilder =>
        {
            nameBuilder.Property(n => n.Name)
                .HasColumnName("Name")
                .IsRequired();
        });

        // Outras Propriedades
        builder.Property(p => p.AwsKey)
            .HasColumnName("AwsKey")
            .IsRequired();

        builder.Property(p => p.UrlTemp)
            .HasColumnName("UrlTemp")
            .IsRequired();

        builder.Property(p => p.UrlExpired)
            .HasColumnName("UrlExpired")
            .IsRequired();

        builder.Property(p => p.Ativo)
            .HasColumnName("Ativo")
            .IsRequired();

        // Nome da tabela
        builder.ToTable("Pictures");
    }
}

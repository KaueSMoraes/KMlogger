using System;
using Domain.Entities;
using Infrastructure.Data.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.FluentMapping;

public  class LogMapping : IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        // Nome da tabela
        builder.ToTable("Logs");
        // Configuração da chave primária
        builder.HasKey(l => l.Id).HasName("PK_Logs");

        builder.Property(l => l.Id)
            .HasColumnName("Id")
            .HasColumnType("uuid")
            .IsRequired().ValueGeneratedOnAdd();

        builder.Property(l => l.CreatedDate)
            .HasColumnName("CreatedDate")
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(l => l.UpdatedDate)
            .HasColumnName("UpdatedDate")
            .HasColumnType("timestamp")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAddOrUpdate();

        builder.Property(l => l.DeletedDate)
            .HasColumnName("DeletedDate")
            .HasColumnType("timestamp").IsRequired(false);

        builder.Property(l => l.Environment)
            .HasColumnName("Environment")
            .IsRequired();

        builder.Property(l => l.Level)
                .HasColumnName("Level")
                .HasConversion(new EnumConverter()) 
                .IsRequired();


        builder.OwnsOne(l => l.Message, messageBuilder =>
        {
            messageBuilder.Property(m => m.Text)
                .HasColumnName("Message")
                .IsRequired();
        });

        builder.OwnsOne(l => l.StackTrace, stackTraceBuilder =>
        {
            stackTraceBuilder.Property(s => s.Text)
                .HasColumnName("StackTrace")
                .IsRequired(false);
        });
    }
}

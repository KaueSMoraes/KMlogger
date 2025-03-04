﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(KMLoggerDbContext))]
    [Migration("20250215222845_InitialCreateDb")]
    partial class InitialCreateDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Picture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean")
                        .HasColumnName("Ativo");

                    b.Property<string>("AwsKey")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("AwsKey");

                    b.Property<DateTime>("UrlExpired")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UrlExpired");

                    b.Property<string>("UrlTemp")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("UrlTemp");

                    b.HasKey("Id");

                    b.ToTable("Pictures", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("CreatedDate");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("DeletedDate");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Slug");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("UpdatedDate");

                    b.HasKey("Id")
                        .HasName("PK_Roles");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean")
                        .HasColumnName("Active");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("CreatedDate");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("DeletedDate");

                    b.Property<long>("TokenActivate")
                        .HasColumnType("bigint")
                        .HasColumnName("TokenActivate");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("UpdatedDate");

                    b.HasKey("Id")
                        .HasName("PK_Users");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("UserRole", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Domain.Entities.Picture", b =>
                {
                    b.OwnsOne("Domain.ValueObjects.UniqueName", "Name", b1 =>
                        {
                            b1.Property<Guid>("PictureId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Name");

                            b1.HasKey("PictureId");

                            b1.ToTable("Pictures");

                            b1.WithOwner()
                                .HasForeignKey("PictureId");
                        });

                    b.OwnsOne("Domain.ValueObjects.AppFile", "File", b1 =>
                        {
                            b1.Property<Guid>("PictureId")
                                .HasColumnType("uuid");

                            b1.Property<string>("FileName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("FileName");

                            b1.Property<long>("FileSize")
                                .HasColumnType("bigint")
                                .HasColumnName("FileSize");

                            b1.HasKey("PictureId");

                            b1.ToTable("Pictures");

                            b1.WithOwner()
                                .HasForeignKey("PictureId");
                        });

                    b.Navigation("File")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.OwnsOne("Domain.ValueObjects.UniqueName", "Name", b1 =>
                        {
                            b1.Property<Guid>("RoleId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.HasKey("RoleId");

                            b1.ToTable("Roles");

                            b1.WithOwner()
                                .HasForeignKey("RoleId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.OwnsOne("Domain.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Complement")
                                .HasMaxLength(100)
                                .HasColumnType("varchar")
                                .HasColumnName("Complement");

                            b1.Property<string>("NeighBordHood")
                                .HasColumnType("varchar")
                                .HasColumnName("NeighborHood");

                            b1.Property<long?>("Number")
                                .HasColumnType("BIGINT")
                                .HasColumnName("Number");

                            b1.Property<string>("Road")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("Road");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Address")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("Email");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("Domain.ValueObjects.FullName", "FullName", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("LastName");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("Domain.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Hash")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Hash");

                            b1.Property<string>("Salt")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Salt");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("FullName")
                        .IsRequired();

                    b.Navigation("Password")
                        .IsRequired();
                });

            modelBuilder.Entity("UserRole", b =>
                {
                    b.HasOne("Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserRole_RoleId");

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserRole_UserId");
                });
#pragma warning restore 612, 618
        }
    }
}

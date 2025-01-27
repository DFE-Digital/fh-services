﻿// <auto-generated />
using System;
using System.ComponentModel.DataAnnotations;
using FamilyHubs.Idam.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FamilyHubs.Idam.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250124104651_AddAdminUICache")]
    partial class AddAdminUICache
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FamilyHubs.Idam.Data.Entities.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("Created")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)")
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.IsEncrypted", true)
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.StorageFormat", StorageFormat.Default);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)")
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.IsEncrypted", true)
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.StorageFormat", StorageFormat.Default);

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)")
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.IsEncrypted", true)
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.StorageFormat", StorageFormat.Default);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)")
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.IsEncrypted", true)
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.StorageFormat", StorageFormat.Default);

                    b.Property<string>("OpenId")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.IsEncrypted", true)
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.StorageFormat", StorageFormat.Default);

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("FamilyHubs.Idam.Data.Entities.AccountClaim", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Created")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)")
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.IsEncrypted", true)
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.StorageFormat", StorageFormat.Default);

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)")
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.IsEncrypted", true)
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.StorageFormat", StorageFormat.Default);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountClaims");
                });

            modelBuilder.Entity("FamilyHubs.Idam.Data.Entities.UserSession", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("Created")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)")
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.IsEncrypted", true)
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.StorageFormat", StorageFormat.Default);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)")
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.IsEncrypted", true)
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.StorageFormat", StorageFormat.Default);

                    b.Property<DateTime?>("LastActive")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)")
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.IsEncrypted", true)
                        .HasAnnotation("Microsoft.EntityFrameworkCore.DataEncryption.StorageFormat", StorageFormat.Default);

                    b.Property<string>("Sid")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("UserSessions");
                });

            modelBuilder.Entity("FamilyHubs.Idam.Data.Entities.AccountClaim", b =>
                {
                    b.HasOne("FamilyHubs.Idam.Data.Entities.Account", null)
                        .WithMany("Claims")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FamilyHubs.Idam.Data.Entities.Account", b =>
                {
                    b.Navigation("Claims");
                });
#pragma warning restore 612, 618
        }
    }
}

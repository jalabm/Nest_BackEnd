﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nest_6._03.Data;

#nullable disable

namespace Nest_6._03.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240312022404_softDeleteAdded")]
    partial class softDeleteAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Nest_6._03.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SoftDelete")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("Nest_6._03.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("DiscountPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Rating")
                        .HasColumnType("float");

                    b.Property<decimal>("SellPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("VendorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("VendorId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("Nest_6._03.Models.ProductImg", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsHower")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<int>("ProdictId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("productimgs");
                });

            modelBuilder.Entity("Nest_6._03.Models.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<double?>("Rating")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("vendors");
                });

            modelBuilder.Entity("Nest_6._03.Models.Product", b =>
                {
                    b.HasOne("Nest_6._03.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nest_6._03.Models.Vendor", "Vendor")
                        .WithMany("Products")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("Nest_6._03.Models.ProductImg", b =>
                {
                    b.HasOne("Nest_6._03.Models.Product", "Product")
                        .WithMany("ProductImgs")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Nest_6._03.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Nest_6._03.Models.Product", b =>
                {
                    b.Navigation("ProductImgs");
                });

            modelBuilder.Entity("Nest_6._03.Models.Vendor", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}

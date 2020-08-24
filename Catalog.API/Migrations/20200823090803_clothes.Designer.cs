﻿// <auto-generated />
using Catalog.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Catalog.API.Migrations
{
    [DbContext(typeof(CatalogContext))]
    [Migration("20200823090803_clothes")]
    partial class clothes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Catalog.API.Model.CatalogBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CatalogBrands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "Nokia"
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Apple"
                        },
                        new
                        {
                            Id = 3,
                            Brand = "DeFacto"
                        },
                        new
                        {
                            Id = 4,
                            Brand = "Network"
                        });
                });

            modelBuilder.Entity("Catalog.API.Model.CatalogItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvailableStock")
                        .HasColumnType("int");

                    b.Property<int>("CatalogBrandId")
                        .HasColumnType("int");

                    b.Property<int>("CatalogTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxStockThreshold")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OnReorder")
                        .HasColumnType("bit");

                    b.Property<string>("PictureFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PictureUri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RestockThreshold")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CatalogItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AvailableStock = 50,
                            CatalogBrandId = 1,
                            CatalogTypeId = 2,
                            Description = "Lumia 1520",
                            MaxStockThreshold = 100,
                            Name = "Nokia Lumia 1520",
                            OnReorder = false,
                            PictureFileName = "",
                            PictureUri = "",
                            Price = 999m,
                            RestockThreshold = 3
                        },
                        new
                        {
                            Id = 2,
                            AvailableStock = 25,
                            CatalogBrandId = 2,
                            CatalogTypeId = 2,
                            Description = "iPhone 8 Plus",
                            MaxStockThreshold = 500,
                            Name = "iPhone 8 Plus",
                            OnReorder = false,
                            PictureFileName = "",
                            PictureUri = "",
                            Price = 1499m,
                            RestockThreshold = 20
                        },
                        new
                        {
                            Id = 3,
                            AvailableStock = 300,
                            CatalogBrandId = 3,
                            CatalogTypeId = 1,
                            Description = "Polo T-Shirt",
                            MaxStockThreshold = 500,
                            Name = "Polo T-Shirt",
                            OnReorder = false,
                            PictureFileName = "",
                            PictureUri = "",
                            Price = 24.9m,
                            RestockThreshold = 50
                        },
                        new
                        {
                            Id = 4,
                            AvailableStock = 250,
                            CatalogBrandId = 4,
                            CatalogTypeId = 1,
                            Description = "White Pants",
                            MaxStockThreshold = 500,
                            Name = "White Pants",
                            OnReorder = false,
                            PictureFileName = "",
                            PictureUri = "",
                            Price = 44.9m,
                            RestockThreshold = 30
                        });
                });

            modelBuilder.Entity("Catalog.API.Model.CatalogType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CatalogTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Type = "Clothes"
                        },
                        new
                        {
                            Id = 2,
                            Type = "Electronics"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

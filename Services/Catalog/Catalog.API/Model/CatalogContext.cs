using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.API.Model
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new Exception("Simulated dummy exception.");
            base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogBrand>().HasData(new CatalogBrand()
            {
                Id = 1,
                Brand = "Nokia"
            });


            modelBuilder.Entity<CatalogBrand>().HasData(new CatalogBrand()
            {
                Id = 2,
                Brand = "Apple"
            });

            modelBuilder.Entity<CatalogBrand>().HasData(new CatalogBrand()
            {
                Id = 3,
                Brand = "DeFacto"
            });

            modelBuilder.Entity<CatalogBrand>().HasData(new CatalogBrand()
            {
                Id = 4,
                Brand = "Network"
            });


            modelBuilder.Entity<CatalogType>().HasData(new CatalogType()
            {
                Id = 1,
                Type = "Clothes"
            });

            modelBuilder.Entity<CatalogType>().HasData(new CatalogType()
            {
                Id = 2,
                Type = "Electronics"
            });


          
            modelBuilder.Entity<CatalogItem>().HasData(new CatalogItem()
            {
                Id = 1,
                PictureFileName = "",
                AvailableStock = 50,
                CatalogBrandId = 1,
                CatalogTypeId = 2,
                Description = "Lumia 1520",
                MaxStockThreshold = 100,
                Name = "Nokia Lumia 1520",
                OnReorder = false,
                PictureUri = "",
                Price = 999,
                RestockThreshold = 3
            });

            modelBuilder.Entity<CatalogItem>().HasData(new CatalogItem()
            {
                Id = 2,
                PictureFileName = "",
                AvailableStock = 25,
                CatalogBrandId = 2,
                CatalogTypeId = 2,
                Description = "iPhone 8 Plus",
                MaxStockThreshold = 500,
                Name = "iPhone 8 Plus",
                OnReorder = false,
                PictureUri = "",
                Price = 1499,
                RestockThreshold = 20
            });

            modelBuilder.Entity<CatalogItem>().HasData(new CatalogItem()
            {
                Id = 3,
                PictureFileName = "",
                AvailableStock = 300,
                CatalogBrandId = 3,
                CatalogTypeId = 1,
                Description = "Polo T-Shirt",
                MaxStockThreshold = 500,
                Name = "Polo T-Shirt",
                OnReorder = false,
                PictureUri = "",
                Price = 24.9M,
                RestockThreshold = 50
            });


            modelBuilder.Entity<CatalogItem>().HasData(new CatalogItem()
            {
                Id = 4,
                PictureFileName = "",
                AvailableStock = 250,
                CatalogBrandId = 4,
                CatalogTypeId = 1,
                Description = "White Pants",
                MaxStockThreshold = 500,
                Name = "White Pants",
                OnReorder = false,
                PictureUri = "",
                Price = 44.9M,
                RestockThreshold = 30
            });


        }

        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }


    }
}

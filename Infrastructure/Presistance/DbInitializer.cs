using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presistance.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistance
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreHubDbContext context;

        public DbInitializer(StoreHubDbContext _context)
        {
            context = _context;
        }
        public async Task Initializer()
        {
            //Create Database if it doesn't Exist && Apply Any Migrations
            if (context.Database.GetPendingMigrations().Any()) 
            {
                await context.Database.MigrateAsync();
            }

            //DataSeeding
            //Seeding Types
            if (!context.ProductTypes.Any()) 
            {
                // 1. Read All Data From types Json File as String
                var TypeData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Seeding\types.json");

                // 2. Transform String To C# Objects [List<ProductTypes>]
                var models = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
  ;              // 3. Add List<ProductTypes> To Database
                if (models is not null && models.Any()) 
                {
                    await context.ProductTypes.AddRangeAsync(models);
                    await context.SaveChangesAsync();
                }
            }

            //Seeding Brands
            if (!context.ProductBrands.Any())
            {
                // 1. Read All Data From types Json File as String
                var BrandsTypes = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Seeding\brands.json");

                // 2. Transform String To C# Objects [List<ProductTypes>]
                var models =  JsonSerializer.Deserialize<List<ProductBrand>>(BrandsTypes);
                // 3. Add List<ProductTypes> To Database
                if (models is not null && models.Any()) 
                {
                    await context.ProductBrands.AddRangeAsync(models);
                    await context.SaveChangesAsync();
                }
            }

            //Seeding Products
            if (!context.Products.Any()) 
            {
                // Read All Data From Product Json
                var ProductData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistance\Seeding\products.json");
                //Transform Data From Json To C# Object
                var models = JsonSerializer.Deserialize<List<Product>>(ProductData);

                //Add Data To DataBase
                if (models is not null && models.Any()) 
                {
                    await context.Products.AddRangeAsync(models);
                    await context.SaveChangesAsync();
                }
            }


        }
    }
}

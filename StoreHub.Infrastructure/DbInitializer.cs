using Domain.Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistance.Data;
using StoreHub.Core.Models.Identity;
using StoreHub.Core.Models.Orders;
using StoreHub.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistance
{
    public class DbInitializer(StoreHubDbContext context, StoreHubIdentityDbContext identityDbContext,
        RoleManager<IdentityRole> roles, UserManager<AppUser> users) : IDbInitializer
    {


        public async Task IdentityInitializer()
        {
            if (identityDbContext.Database.GetPendingMigrations().Any())
            {
                await identityDbContext.Database.MigrateAsync();
            }

            //SeedRoles
            if (!roles.Roles.Any())
            {
                await roles.CreateAsync(new IdentityRole("Admin"));
                await roles.CreateAsync(new IdentityRole("SuperAdmin"));
            }
            if (!users.Users.Any())
            {
                var superAdmin = new AppUser()
                {
                    DisplayName = "SuperAdmin",
                    UserName = "superadmin",
                    Email = "SuperAdmin@gmail.com",
                    PhoneNumber = "010123456769",
                };
                var Admin = new AppUser()
                {
                    DisplayName = "Admin",
                    UserName = "admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "010123456769",
                };

                await roles.CreateAsync(new IdentityRole("Admin"));
                await roles.CreateAsync(new IdentityRole("SuperAdmin"));
                await users.CreateAsync(superAdmin, "P@ssw0rd");
                await users.CreateAsync(Admin, "P@ssw0rd");
                await users.AddToRoleAsync(superAdmin, "SuperAdmin");
                await users.AddToRoleAsync(Admin, "Admin");

            }

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
                var TypeData = await File.ReadAllTextAsync(@"..\StoreHub.Infrastructure\Seeding\types.json");

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
                var BrandsTypes = await File.ReadAllTextAsync(@"..\StoreHub.Infrastructure\Seeding\brands.json");

                // 2. Transform String To C# Objects [List<ProductTypes>]
                var models = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsTypes);
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
                var ProductData = await File.ReadAllTextAsync(@"..\StoreHub.Infrastructure\Seeding\products.json");
                //Transform Data From Json To C# Object
                var models = JsonSerializer.Deserialize<List<Product>>(ProductData);

                //Add Data To DataBase
                if (models is not null && models.Any())
                {
                    await context.Products.AddRangeAsync(models);
                    await context.SaveChangesAsync();
                }
            }

            //Seeding DeliverMethods
            if (!context.DeliveryMethods.Any())
            {
                // Read All Data From Product Json
                var deliverMehtods = await File.ReadAllTextAsync(@"..\StoreHub.Infrastructure\Seeding\delivery.json");
                //Transform Data From Json To C# Object
                var models = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliverMehtods);

                //Add Data To DataBase
                if (models is not null && models.Any())
                {
                    await context.DeliveryMethods.AddRangeAsync(models);
                    await context.SaveChangesAsync();
                }
            }


        }
    }
}

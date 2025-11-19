using Domain.Contracts;
using Domain.Entities.Identity_Modules;
using Domain.Entities.ProductModules;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Persistance.Data.Seeding
{
    public class DataSeeding(StoreContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            var PendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (PendingMigrations.Any())
            {
                dbContext.Database.Migrate();
            }

            #region Seeding Data

            #region ProductBrand
            if (!dbContext.ProductBrands.Any())
            {
                var BrandsData = File.OpenRead(@"..\Infrastructure\Persistance\Data\Seeding\brands.json");
                var Brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(BrandsData);
                if (Brands is not null && Brands.Any())
                {
                    await dbContext.ProductBrands.AddRangeAsync(Brands);


                }
            }
            #endregion

            #region ProductType
            if (!dbContext.ProductTypes.Any())
            {
                var TypesData = File.OpenRead(@"..\Infrastructure\Persistance\Data\Seeding\types.json");
                var Types = await JsonSerializer.DeserializeAsync<List<ProductType>>(TypesData);
                if (Types is not null && Types.Any())
                {
                    await dbContext.ProductTypes.AddRangeAsync(Types);


                }
            }

            #endregion

            #region Products

            if (!dbContext.Products.Any())
            {
                var ProductsData = File.OpenRead(@"..\Infrastructure\Persistance\Data\Seeding\products.json");
                var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                if (Products is not null && Products.Any())
                {
                    await dbContext.Products.AddRangeAsync(Products);


                }
            }

            #endregion


            await dbContext.SaveChangesAsync();

            #endregion
        }

        public async Task IdentityDataSeed()
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));

                }
                if (!userManager.Users.Any())
                {
                    var user01 = new ApplicationUser()
                    {
                        DisplayName = "Yasmin Hossam",
                        Email = "yasmin@gmail.com",
                        PhoneNumber = "01000000000",
                        UserName = "YasminHossam",
                        EmailConfirmed = true,
                    };
                    var user02 = new ApplicationUser()
                    {
                        DisplayName = "Mina Samy",
                        Email = "Mina@gmail.com",
                        PhoneNumber = "01000000011",
                        UserName = "Minasamy",
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(user01, "Pa$$w0rd");
                    await userManager.CreateAsync(user02, "Pa$$w0rd");
                    //--------------------
                    //Add To Role
                    await userManager.AddToRoleAsync(user01, "Admin");
                    await userManager.AddToRoleAsync(user02, "SuperAdmin");

                }


            }
            catch (Exception)
            {

                throw;
            }
        }


        } 
    
    }


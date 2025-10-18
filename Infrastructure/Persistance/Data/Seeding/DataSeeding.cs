using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Persistance.Data.Seeding
{
    public class DataSeeding(StoreContext dbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            var PendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (PendingMigrations.Any() )
            {
                dbContext.Database.Migrate();
            }

            #region Seeding Data

            #region ProductBrand
            if (!dbContext.ProductBrands.Any())
            {
                var BrandsData = File.OpenRead(@"..\Infrastructure\Persistance\Data\Seeding\brands.json");
                var Brands =await JsonSerializer.DeserializeAsync<List<ProductBrand>>(BrandsData);
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
    }
}

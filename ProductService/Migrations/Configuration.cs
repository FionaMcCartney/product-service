namespace ProductService.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using ProductService.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ProductService.Models.ProductServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProductService.Models.ProductServiceContext context)
        {
            context.Categories.AddOrUpdate(x => x.Id,
              new Category() { Id = 1, Name = "Cocktails" },
              new Category() { Id = 2, Name = "Beers" },
              new Category() { Id = 3, Name = "Wines" }
              );

            //TODO: Seed database from JSON file in App_Data.
            //var jsonFilePath = @"C:\Users\fiona\source\repos\ProductService\ProductService\App_Data\products.json";

            //var types = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(jsonFilePath));
            //context.Products.AddRange(types);
            //context.SaveChanges();

            context.Products.AddOrUpdate(x => x.Id,
                new Product()
                {
                    Id = 1,
                    Name = "Shot of Goldschläger",
                    CategoryId = 1,
                    Description = "chilled liqueur with tiny, floating, gold flakes"
                },
               new Product()
               {
                   Id = 2,
                   Name = "English India Pale Ale",
                   CategoryId = 2,
                   Description = "First brewed in England and exported for the British troops in India during the late 1700s. To withstand the voyage, IPA's were basically tweaked Pale Ales that were, in comparison, much more malty, boasted a higher alcohol content and were well-hopped, as hops are a natural preservative. Historians believe that an IPA was then watered down for the troops, while officers and the elite would savor the beer at full strength. The English IPA has a lower alcohol due to taxation over the decades. The leaner the brew the less amount of malt there is and less need for a strong hop presence which would easily put the brew out of balance. Some brewers have tried to recreate the origianl IPA with strengths close to 8-9% abv."
               },
               new Product()
               {
                   Id = 3,
                   Name = "Pinot Grigio",
                   CategoryId = 3,
                   Description = "Pinot Grigio is one of the world's most popular wines. It's also known by a few different names around the world, including 'Pinot Gris' in France 'Ruländer' in Germany."
               }
              );
        }

    }
}

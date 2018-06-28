namespace ProductService.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;
    using ProductService.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ProductService.Models.ProductServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProductService.Models.ProductServiceContext context)
        {
            GetCategories(context);
            GetProducts(context);
        }

        private void GetCategories(ProductServiceContext context)
        {
            context.Categories.AddOrUpdate(x => x.Id,
             new Category() { Id = 1, Name = "Cocktails" },
             new Category() { Id = 2, Name = "Beers" },
             new Category() { Id = 3, Name = "Wines" },
             new Category() { Id = 4, Name = "Spirits" }
             );
        }

        private void GetProducts(ProductServiceContext context)
        {
            //TODO: Replace file path with the directory you are running the code from.
            string jsonFilePath = @"C:\Users\fiona.mccartney\Source\Repos\ProductService\ProductService\App_Data\products.json";
            string productJsonAll = GetEmbeddedResourceAsString(jsonFilePath);

            JArray jsonValProducts = JArray.Parse(productJsonAll) as JArray;
            dynamic productsData = jsonValProducts;

            foreach (dynamic product in productsData)
            {
                context.Products.AddOrUpdate(x => x.Id,
                    new Product
                    {
                        Id = product.id,
                        Name = product.name,
                        Description = product.description,
                        CategoryId = product.categoryId
                    }
                 );
            }
        }

        private string GetEmbeddedResourceAsString(string resourceName)
        {
            string result;

            using (StreamReader reader = new StreamReader(resourceName))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

    }
}

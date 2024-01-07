using ESourcing.Products.Entities;
using MongoDB.Driver;

namespace ESourcing.Products.Data
{
    public class ProductContextSeed
    {
        public static void SeedData(IMongoCollection<Product> Products)
        {
            bool isCollectionExists = Products.Find(_ => true).Any();
            if (!isCollectionExists)
            {
                Products.InsertManyAsync(GetConfigureProducts());
            }
        }

        private static IEnumerable<Product> GetConfigureProducts()
        {
            return new List<Product>()
            {
                new Product()
                { 
                    Name = "Samsung 10",
                    Summary = "Samsung Smart Phone",
                    Description = "Samsung Smart Phone Description",
                    ImageFile = "product-1.png",
                    Price = 840.00M,
                    Category = "Smart Phone"
                },
                new Product()
                {
                    Name = "Iphone 14",
                    Summary = "Iphone Smart Phone",
                    Description = "Iphone Smart Phone Description",
                    ImageFile = "product-2.png",
                    Price = 1260.00M,
                    Category = "Smart Phone"
                }
            };
        }
    }
}

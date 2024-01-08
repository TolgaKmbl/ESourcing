using ESourcing.Products.Configuration.MongoDb;
using ESourcing.Products.Data.Contract;
using ESourcing.Products.Entities;
using MongoDB.Driver;

namespace ESourcing.Products.Data
{
    public class ProductContext : IProductContext
    {

        public IProductMongoDbSettings? ProductMongoDbSettings { get; set; }

        public ProductContext(IProductMongoDbSettings productMongoDbSettings)
        {
            var client = new MongoClient(productMongoDbSettings.ConnectionString);
            var database = client.GetDatabase(productMongoDbSettings.DatabaseName);

            Products = database.GetCollection<Product>(productMongoDbSettings.CollectionName);
            ProductContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}

namespace ESourcing.Products.Configuration
{
    public class ProductMongoDbSettings : IProductMongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}

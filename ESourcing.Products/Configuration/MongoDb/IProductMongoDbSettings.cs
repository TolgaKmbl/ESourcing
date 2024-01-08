namespace ESourcing.Products.Configuration.MongoDb
{
    public interface IProductMongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}

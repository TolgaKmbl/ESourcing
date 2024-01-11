namespace ESourcing.Products.Configuration.MongoDb
{
    public interface ISourcingMongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

namespace ESourcing.Products.Configuration.MongoDb
{
    public class SourcingMongoDbSettings : ISourcingMongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

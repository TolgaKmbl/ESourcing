using ESourcing.Products.Configuration.MongoDb;
using ESourcing.Products.Data.Contract;
using ESourcing.Sourcing.Entities;
using MongoDB.Driver;

namespace ESourcing.Products.Data
{
    public class SourcingContext : ISourcingContext
    {

        public ISourcingMongoDbSettings? SourcingMongoDbSettings { get; set; }

        public SourcingContext(ISourcingMongoDbSettings SourcingMongoDbSettings)
        {
            var client = new MongoClient(SourcingMongoDbSettings.ConnectionString);
            var database = client.GetDatabase(SourcingMongoDbSettings.DatabaseName);

            Auctions = database.GetCollection<Auction>(nameof(Auction));
            Bids = database.GetCollection<Bid>(nameof(Bid));
        }

        public IMongoCollection<Auction> Auctions { get; }
        public IMongoCollection<Bid> Bids { get; }
    }
}

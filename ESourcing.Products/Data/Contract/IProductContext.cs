using ESourcing.Products.Entities;
using MongoDB.Driver;

namespace ESourcing.Products.Data.Contract
{
    public interface IProductContext
    {
        IMongoCollection<Product> Products { get; }
    }
}

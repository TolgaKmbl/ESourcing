using ESourcing.Products.Data.Contract;
using ESourcing.Products.Entities;
using ESourcing.Products.Repositories.Contract;
using MongoDB.Driver;

namespace ESourcing.Products.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly IProductContext _productContext;

        public ProductRepository(IProductContext productContext)
        {
            this._productContext = productContext;
        }

        public async Task Create(Product product)
        {
            await _productContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> Delete(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _productContext.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _productContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productContext.Products.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string productCategory)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Category, productCategory);
            return await _productContext.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string productName)
        {
            var filter = Builders<Product>.Filter.ElemMatch(p => p.Name, productName);
            return await _productContext.Products.Find(filter).ToListAsync();
        }

        public async Task<bool> Update(Product product)
        {
            var updateResult = await _productContext.Products.ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}

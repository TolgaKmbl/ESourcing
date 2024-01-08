using ESourcing.Products.Configuration.MongoDb;
using ESourcing.Products.Data.Contract;
using ESourcing.Products.Data;
using ESourcing.Products.Repositories.Contract;
using ESourcing.Products.Repositories;
using Microsoft.Extensions.Options;

namespace ESourcing.Products.Configuration
{
    public static class ServiceConfiguration
    {
        public static void ConfigureMongoDb(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.Configure<ProductMongoDbSettings>(configuration.GetSection(nameof(ProductMongoDbSettings)));
            services.AddSingleton<IProductMongoDbSettings>(sp => sp.GetRequiredService<IOptions<ProductMongoDbSettings>>().Value);
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IProductContext, ProductContext>();
            services.AddSingleton<IProductRepository, ProductRepository>();
        }
    }
}

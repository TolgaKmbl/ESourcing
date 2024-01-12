
using ESourcing.Products.Configuration.MongoDb;
using ESourcing.Products.Data;
using ESourcing.Products.Data.Contract;
using ESourcing.Sourcing.Repositories;
using ESourcing.Sourcing.Repositories.Contract;
using Microsoft.Extensions.Options;

namespace ESourcing.Products.Configuration
{
    public static class ServiceConfiguration
    {
        public static void ConfigureMongoDb(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.Configure<SourcingMongoDbSettings>(configuration.GetSection(nameof(SourcingMongoDbSettings)));
            services.AddSingleton<ISourcingMongoDbSettings>(sp => sp.GetRequiredService<IOptions<SourcingMongoDbSettings>>().Value);
        }

        public static void ConfigureContext(this IServiceCollection services)
        {
            services.AddSingleton<ISourcingContext, SourcingContext>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IAuctionRepository, AuctionRepository>();
            services.AddSingleton<IBidRepository, BidRepository>();
        }
    }
}

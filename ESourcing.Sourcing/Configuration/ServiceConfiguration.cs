
using AutoMapper;
using ESourcing.Products.Configuration.MongoDb;
using ESourcing.Products.Data;
using ESourcing.Products.Data.Contract;
using ESourcing.Sourcing.Mapping;
using ESourcing.Sourcing.Repositories;
using ESourcing.Sourcing.Repositories.Contract;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Producer;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

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

        public static void ConfigureRabbitMQ(this IServiceCollection services,
           IConfiguration configuration)
        {
            _ = services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = configuration["EventBus:HostName"]
                };

                if (!string.IsNullOrWhiteSpace(configuration["EventBus:UserName"]))
                {
                    factory.UserName = configuration["EventBus:UserName"];
                }

                if (!string.IsNullOrWhiteSpace(configuration["EventBus:Password"]))
                {
                    factory.UserName = configuration["EventBus:Password"];
                }

                var retryCount = 5;
                if (!string.IsNullOrWhiteSpace(configuration["EventBus:RetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBus:RetryCount"]);
                }

                return new RabbitMQPersistentConnection(factory, retryCount, logger);
            });

            services.AddSingleton<EventBusRabbitMQProducer>();

        }
    }
}

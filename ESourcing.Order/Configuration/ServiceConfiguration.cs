using EventBusRabbitMQ.Producer;
using EventBusRabbitMQ;
using RabbitMQ.Client;
using ESourcing.Order.Consumers;

namespace ESourcing.Order.Configuration
{
    public static class ServiceConfiguration
    {
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

                if (!string.IsNullOrWhiteSpace(configuration["EventBus:Username"]))
                {
                    factory.UserName = configuration["EventBus:Username"];
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

            services.AddSingleton<EventBusOrderCreateConsumer>();

        }
    }
}

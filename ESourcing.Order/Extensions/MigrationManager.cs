﻿using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Data;

namespace ESourcing.Order.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    //To get the required service from dependency injection
                    var orderContext = scope.ServiceProvider.GetRequiredService<OrderContext>();

                    if (orderContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                    {
                        orderContext.Database.Migrate();
                    }

                    OrderContextSeed.SeedAsync(orderContext).Wait();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return host;
        }
    }
}

using LocksSearch.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NRediSearch;
using StackExchange.Redis;
using System.Linq;
using static NRediSearch.Client;
using static LocksSearch.Helpers.RediSearchHelper;
using LocksSearch.Helpers;

namespace LocksSearch.Extensions
{
    public static class RediSearchExtensions
    {
        public static void AddRediSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = ConnectionMultiplexer.Connect(configuration["RediSearch:Connection"]);
            var database = connection.GetDatabase();
            var client = new Client(configuration["RediSearch:IndexName"], database);
            services.AddSingleton(client);
        }

        public static IWebHost SeedRediSearch(this IWebHost webHost)
        {
            var serviceScopeFactory = (IServiceScopeFactory)webHost
                .Services.GetService(typeof(IServiceScopeFactory));

            using var scope = serviceScopeFactory.CreateScope();
            var services = scope.ServiceProvider;

            using var dbContext = services.GetRequiredService<ElementsContext>();
            var client = services.GetRequiredService<Client>();

            try
            {
                // if client schema is not created this will throw unknown index exception.
                client.GetInfoParsed();
            } 
            catch
            {
                var schema = CreateSchema();
                client.CreateIndex(schema, new ConfiguredIndexOptions(ConfiguredIndexOptions.Default));

                foreach (var b in dbContext.Buildings.Select(b => ToDocument(b)))
                {
                    client.AddDocument(b);
                }
                foreach (var o in dbContext.Locks.Select(o => ToDocument(o)))
                {
                    client.AddDocument(o);
                }
                foreach (var o in dbContext.Groups.Select(o => ToDocument(o)))
                {
                    client.AddDocument(o);
                }
                foreach (var o in dbContext.Medias.Select(o => ToDocument(o)))
                {
                    client.AddDocument(o);
                }
            }

            return webHost;
        }
    }
}

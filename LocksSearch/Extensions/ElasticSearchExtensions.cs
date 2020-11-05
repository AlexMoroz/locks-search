using LocksSearch.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocksSearch.Extensions
{
    public static class ElasticSearchExtensions
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new ConnectionSettings(new Uri(configuration["ElasticSearch:Url"]));

            settings.AddMappings();

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
        }

        public static IWebHost SeedElasticSearch(this IWebHost webHost)
        {
            var serviceScopeFactory = (IServiceScopeFactory)webHost
                .Services.GetService(typeof(IServiceScopeFactory));

            using var scope = serviceScopeFactory.CreateScope();
            var services = scope.ServiceProvider;

            using var dbContext = services.GetRequiredService<ElementsContext>();
            var client = services.GetRequiredService<IElasticClient>();

            client.Indices.Delete("index");

            client.IndexMany(dbContext.Buildings);
            client.IndexMany(dbContext.Groups);

            return webHost;
        }

        private static void AddMappings(this ConnectionSettings settings)
        {
            settings
                .DefaultIndex("index")
                .DefaultMappingFor<Building>(b => b.Ignore(p => p.Locks).IndexName("index"))
                .DefaultMappingFor<Group>(g => g.Ignore(p => p.Medias).IndexName("index"));
        }
    }
}

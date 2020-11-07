using System;
using System.IO;
using System.Linq;
using LocksSearch.Models;
using LocksSearch.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LocksSearch.Extensions
{
    public static class EntityDbExtentions
    {
        public static IWebHost MigrateDatabase(this IWebHost webHost)
        {
            var serviceScopeFactory = (IServiceScopeFactory)webHost
                .Services.GetService(typeof(IServiceScopeFactory));

            using var scope = serviceScopeFactory.CreateScope();
            var services = scope.ServiceProvider;

            using var dbContext = services.GetRequiredService<ElementsContext>();
            dbContext.Database.Migrate();

            return webHost;
        }

        public static IWebHost SeedDatabase(this IWebHost webHost)
        {
            var serviceScopeFactory = (IServiceScopeFactory)webHost
                .Services.GetService(typeof(IServiceScopeFactory));

            using var scope = serviceScopeFactory.CreateScope();
            var services = scope.ServiceProvider;

            using var dbContext = services.GetRequiredService<ElementsContext>();

            var filePath = Path.Combine("Assets", "sv_lsm_data.json");
            var jsonData = new JsonDataImporter(filePath);

            if (!dbContext.Buildings.Any())
            {
                dbContext.Buildings.AddRange(jsonData.Buildings);
                dbContext.SaveChanges();
            }

            if (!dbContext.Locks.Any())
            {
                foreach(var block in jsonData.Locks) {
                    var building = dbContext.Buildings.FirstOrDefault(b => b.Guid == block.BuildingGuid);

                    if (building != null)
                    {
                        block.Building = building;
                        dbContext.Locks.Add(block);
                    }
                    else
                    {
                        dbContext.Locks.Add(block);
                    }
                }
            }

            if (!dbContext.Groups.Any())
            {
                dbContext.Groups.AddRange(jsonData.Groups);
                dbContext.SaveChanges();
            }

            if (!dbContext.Medias.Any())
            {
                foreach (var media in jsonData.Medias)
                {
                    var group = dbContext.Groups.FirstOrDefault(b => b.Guid == media.GroupGuid);

                    if (group != null)
                    {
                        media.Group = group;
                        dbContext.Medias.Add(media);
                    }
                    else
                    {
                        dbContext.Medias.Add(media);
                    }
                }
            }

            dbContext.SaveChanges();

            return webHost;
        }
    }
}

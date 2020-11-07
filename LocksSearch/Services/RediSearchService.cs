using LocksSearch.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NRediSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocksSearch.Services
{
    public class RediSearchService : ISearchService
    {
        private readonly Client _client;
        private readonly ILogger _logger;

        public RediSearchService(Client client, ILogger<RediSearchService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<List<Document>> GetSearchResults(string query)
        {
            var result = await _client.SearchAsync(new Query(query));

            return result.Documents;
        }
    }
}

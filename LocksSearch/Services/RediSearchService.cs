﻿using LocksSearch.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NRediSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LocksSearch.Helpers.RediSearchHelper;

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

        public async Task<IEnumerable<Element>> GetSearchResults(string query, int skip, int take)
        {
            var result = await _client.SearchAsync(new Query($"%%{query}%%|(\"{query}\") => {{ $weight:10;}}").Limit(skip, take));
            return result.Documents.Select(d => CastDocument<Element>(d));
        }
    }
}

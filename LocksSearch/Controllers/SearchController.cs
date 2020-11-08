using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocksSearch.Models;
using LocksSearch.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NRediSearch;

namespace LocksSearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly ILogger<SearchController> _logger;

        public SearchController(ISearchService searchService, ILogger<SearchController> logger)
        {
            _searchService = searchService;
            _logger = logger;
        }

        [HttpGet("find")]
        public Task<IEnumerable<Element>> SearchTerm(string query)
        {
            return _searchService.GetSearchResults(query);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocksSearch.Models;
using LocksSearch.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NRediSearch;

namespace LocksSearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ElementsContext _elementsContext;
        private readonly ISearchService _searchService;
        private readonly ILogger<SearchController> _logger;

        public SearchController(ElementsContext elementsContext, ISearchService searchService, ILogger<SearchController> logger)
        {
            _elementsContext = elementsContext;
            _searchService = searchService;
            _logger = logger;
        }

        [HttpGet("find")]
        public async Task<IEnumerable<dynamic>> SearchTerm(string query)
        {
            //_searchService.GetSearchResults(query);
            var results = await _elementsContext.Locks.ToListAsync();
            return results.Select(l => (dynamic)l);
        }
    }
}

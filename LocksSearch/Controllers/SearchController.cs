using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocksSearch.Models;
using LocksSearch.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LocksSearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;
        private readonly ILogger<SearchController> _logger;

        public SearchController(ISearchService searchService, ILogger<SearchController> logger)
        {
            this.searchService = searchService;
            _logger = logger;
        }

        [HttpGet("find")]
        public Task SearchTerm(string query)
        {
            return searchService.GetSearchResults(query);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocksSearch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LocksSearch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;

        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;
        }

        [HttpGet("search")]
        public Task<List<Element>> SearchTerm(string term)
        {
            return Task.FromResult(new List<Element>());
        }
    }
}

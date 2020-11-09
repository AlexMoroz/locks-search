using LocksSearch.Models;
using Newtonsoft.Json.Linq;
using NRediSearch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocksSearch.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<Dictionary<string, string>>> GetSearchResults(string query, int skip, int take);
    }
}
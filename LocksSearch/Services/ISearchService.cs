using LocksSearch.Models;
using Newtonsoft.Json.Linq;
using NRediSearch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocksSearch.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<Element>> GetSearchResults(string query);
    }
}
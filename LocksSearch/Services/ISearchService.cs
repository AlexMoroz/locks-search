using NRediSearch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocksSearch.Services
{
    public interface ISearchService
    {
        Task<List<Document>> GetSearchResults(string query);
    }
}
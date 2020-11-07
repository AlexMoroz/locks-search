using System.Threading.Tasks;

namespace LocksSearch.Services
{
    public interface ISearchService
    {
        Task GetSearchResults(string text);
    }
}
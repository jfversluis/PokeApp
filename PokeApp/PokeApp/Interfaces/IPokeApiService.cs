using PokeApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokeApp.Interfaces
{
    internal interface IPokeApiService
    {
        Task<IEnumerable<LogItem>> GetFeedAsync();

        Task<IEnumerable<LogItem>> GetFeedAsync(int fromId);

        Task<IEnumerable<LogItem>> GetFeedAsync(int fromId, int limit);

        Task PostFeedItem(LogEntry entry);

        Task<IEnumerable<Pokemon>> GetPokemonAsync();
    }
}
using PokeApp.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokeApp.Interfaces
{
    internal interface IPokeAppApi
    {
        [Get("/feed")]
        [Headers("Authorization: Bearer")]
        Task<IEnumerable<LogItem>> GetFeedAsync();

        [Get("/feed/from/{fromId}")]
        [Headers("Authorization: Bearer")]
        Task<IEnumerable<LogItem>> GetFeedFromIdAsync(int fromId);

        [Get("/feed/from/{fromId}/take/{limit}")]
        [Headers("Authorization: Bearer")]
        Task<IEnumerable<LogItem>> GetFeedFromIdLimitAsync(int fromId, int limit);
    }
}
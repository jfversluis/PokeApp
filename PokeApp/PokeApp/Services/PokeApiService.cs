using Newtonsoft.Json;
using PokeApp.Handlers;
using PokeApp.Interfaces;
using PokeApp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PokeApp.Services
{
    internal class PokeApiService : IPokeApiService
    {
        private IPokeAppApi _pokeApi;

        private IPokeAppApi PokeApi
        {
            get
            {
                return _pokeApi ?? (_pokeApi = RestService.For<IPokeAppApi>(new HttpClient(new AuthenticatedHttpClientHandler(GetToken)) { BaseAddress = new Uri(Constants.BaseApiUrl) }));
            }
        }

        private async Task<string> GetToken()
        {
            // TODO check if current token is expired
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Constants.BaseApiUrl);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Constants.AppIdentifier}:{Constants.AppSecret}")));

                var result = await httpClient.PostAsync("/jwt/token", new MultipartFormDataContent());
                var resultString = await result.Content.ReadAsStringAsync();

                var resultJwt = JsonConvert.DeserializeObject<JwtToken>(resultString);

                return resultJwt.Token;
            }
        }

        public async Task<IEnumerable<LogItem>> GetFeedAsync()
        {
            return await PokeApi.GetFeedAsync();
        }

        public async Task<IEnumerable<LogItem>> GetFeedAsync(int fromId)
        {
            return await GetFeedAsync(fromId, -1);
        }

        public async Task<IEnumerable<LogItem>> GetFeedAsync(int fromId, int limit)
        {
            if (limit == -1)
                return await PokeApi.GetFeedFromIdAsync(fromId);

            return await PokeApi.GetFeedFromIdLimitAsync(fromId, limit);
        }
    }
}
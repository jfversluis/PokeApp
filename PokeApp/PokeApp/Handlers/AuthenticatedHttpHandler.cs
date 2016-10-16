using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace PokeApp.Handlers
{
    internal class AuthenticatedHttpClientHandler : HttpClientHandler
    {
        private readonly Func<Task<string>> _getToken;

        public AuthenticatedHttpClientHandler(Func<Task<string>> getToken)
        {
            if (getToken == null)
                throw new ArgumentNullException(nameof(getToken));

            _getToken = getToken;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // See if the request has an authorize header
            var auth = request.Headers.Authorization;

            if (auth != null)
            {
                var token = await _getToken().ConfigureAwait(false);
                request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, token);
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
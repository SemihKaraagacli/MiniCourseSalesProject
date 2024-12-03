using Microsoft.Extensions.Caching.Memory;
using MiniCourseSalesProject.Web.Models.Dtos;
using MiniCourseSalesProject.Web.Models.Services;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MiniCourseSalesProject.Web.Models.Handler
{
    public class ClientCredentialHandler(IMemoryCache memoryCache, HttpClient client, IConfiguration configuration) : DelegatingHandler
    {
        private const string TokenCacheKey = "tokenCacheKey";
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!memoryCache.TryGetValue(TokenCacheKey, out string token))
            {
                var clientId = configuration.GetSection("Clients")["ClientId"]!;
                var clientSecret = configuration.GetSection("Clients")["ClientSecret"]!;
                var crientialTokenAdress = "/Auth/SignInClientCredential";

                var crientialTokenRequest = new WebClientCredentialRequest(clientId, clientSecret);

                var crientialTokenResponse = await client.PostAsJsonAsync(crientialTokenAdress, crientialTokenRequest);

                if (!crientialTokenResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Client Credential Token not received.");
                }

                var tokenResponse = await crientialTokenResponse.Content.ReadFromJsonAsync<SignInResponse>();
                token = tokenResponse!.AccessToken;

                memoryCache.Set(TokenCacheKey, token); // Cache the token
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // BaseAddress kontrolü
            if (!request.RequestUri!.IsAbsoluteUri && client.BaseAddress == null)
            {
                throw new InvalidOperationException("Request URI must be absolute or BaseAddress must be set.");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}

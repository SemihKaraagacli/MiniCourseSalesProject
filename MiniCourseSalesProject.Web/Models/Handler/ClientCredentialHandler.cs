
using Microsoft.Extensions.Caching.Memory;
using MiniCourseSalesProject.Web.Models.Dtos;
using MiniCourseSalesProject.Web.Models.Services;
using System.Configuration;
using System.Net.Http.Headers;

namespace MiniCourseSalesProject.Web.Models.Handler
{
    public class ClientCredentialHandler(IMemoryCache memoryCache, AuthService authService) : DelegatingHandler
    {
        private const string TokenCacheKey = "tokenCacheKey";
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!memoryCache.TryGetValue(TokenCacheKey, out object token))
            {
                var result = await authService.GetClientCredentialToken();
                if (result.IsError)
                {
                    throw new Exception(result.GetFirstError);
                }
                token = result.Data.AccessToken;
                memoryCache.Set(TokenCacheKey, token);

            }
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
            return await base.SendAsync(request, cancellationToken);
        }
    }
}

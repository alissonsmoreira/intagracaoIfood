using chart_integracao_ifood_infrastructure.Constants;
using chart_integracao_ifood_infrastructure.Gateways;
using chart_integracao_ifood_infrastructure.Models;
using chart_integracao_ifood_infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace chart_integracao_ifood_dal.Handlers
{
    public class AuthHeaderHandler : DelegatingHandler
    {
        private const string TOKEN_KEY = "ifood-access-token";
        private const int TOKEN_EXPIRATION = 3000;
        private const string GRANT_TYPE = "client_credentials";

        private readonly IMemoryCache _cache;
        private readonly IIFoodAuthGateway _gateway;
        private readonly IConfiguration _configuration;
        private readonly IHealthLogService _healthLogService;

        public AuthHeaderHandler(IMemoryCache memoryCache, IIFoodAuthGateway gateway, IConfiguration configuration, IHealthLogService healthLogService)
        {
            _cache = memoryCache;
            _gateway = gateway;
            _configuration = configuration;
            _healthLogService = healthLogService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = ObterToken();

            if (token != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.accessToken);
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        private AccessToken ObterToken()
        {
            var token = GetTokenFromCache();

            if (token != null && !TokenIsExpired(token))
            {
                return token;
            }

            token = GetTokenFromService();

            if (token != null)
            {
                SetTokenInCache(token);
            }

            return token;
        }

        private AccessToken GetTokenFromService()
        {
            var clientId = _configuration["IFoodClientId"];
            var clientSecret = _configuration["IFoodClientSecret"];
            var token = _gateway.GetAccessToken(clientId, clientSecret, GRANT_TYPE).Result;

            if (!token.IsSuccessStatusCode)
            {
                _healthLogService.AddHealthLog(HealthLogCodes.IFOOD_UNAUTHORIZED);
                return null;
            }

            _healthLogService.RemoveHealthLog(HealthLogCodes.IFOOD_UNAUTHORIZED);
            token.Content.created = DateTime.Now;

            return token.Content;
        }

        private static bool TokenIsExpired(AccessToken token)
        {
            return DateTime.Now >= (token.created.AddMilliseconds(token.expiresIn));
        }

        private AccessToken GetTokenFromCache()
        {
            return _cache.Get<AccessToken>(TOKEN_KEY);
        }

        private void SetTokenInCache(AccessToken token)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMilliseconds(TOKEN_EXPIRATION));

            _cache.Set<AccessToken>(TOKEN_KEY, token, cacheEntryOptions);
        }
    }

}

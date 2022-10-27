using chart_integracao_ifood_infrastructure.Entities;
using chart_integracao_ifood_infrastructure.Models;
using chart_integracao_ifood_infrastructure.Models.Common;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace chart_integracao_ifood_infrastructure.Gateways
{
    public interface IIFoodAuthGateway
    {
        [Post("/authentication/v1.0/oauth/token?clientId={clientId}&clientSecret={clientSecret}&grantType={grantType}")]
        [Headers("Content-Type: application/x-www-form-urlencoded")]
        Task<ApiResponse<AccessToken>> GetAccessToken(string clientId, string clientSecret, string grantType);

    }
}

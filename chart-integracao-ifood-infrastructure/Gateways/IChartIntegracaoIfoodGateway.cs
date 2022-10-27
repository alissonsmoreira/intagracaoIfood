using Refit;
using System.Threading.Tasks;

namespace chart_integracao_ifood_infrastructure.Gateways
{
    public interface IChartIntegracaoIfoodGateway
    {
        [Get("/health")]
        public Task<ApiResponse<string>> Health();
    }
}

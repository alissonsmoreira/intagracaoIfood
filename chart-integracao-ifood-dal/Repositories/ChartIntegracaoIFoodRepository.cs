using chart_integracao_ifood_infrastructure.Gateways;
using chart_integracao_ifood_infrastructure.Models.Common;
using chart_integracao_ifood_infrastructure.Repositories;
using System.Threading.Tasks;

namespace chart_integracao_ifood_dal.Repositories
{
    public class ChartIntegracaoIFoodRepository : IChartIntegracaoIFoodRepository
    {
        IChartIntegracaoIfoodGateway _gateway;

        public ChartIntegracaoIFoodRepository(IChartIntegracaoIfoodGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<Result<string>> GetHealth()
        {
            var response = await _gateway.Health();

            return response.IsSuccessStatusCode ? Result<string>.Ok(response.Content) : Result<string>.Erro(response.Error.Content);
        }
    }
}

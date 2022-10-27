using chart_integracao_ifood_infrastructure.Models.Common;
using chart_integracao_ifood_infrastructure.Repositories;
using chart_integracao_ifood_infrastructure.Services;
using System.Threading.Tasks;

namespace chart_integracao_ifood_business.Services
{
    public class ChartIntegracaoIFoodService : IChartIntegracaoIFoodService
    {
        private IChartIntegracaoIFoodRepository _repository;

        public ChartIntegracaoIFoodService(IChartIntegracaoIFoodRepository repository)
        {
            _repository = repository;
        }

        public Task<Result<string>> GetHealth()
        {
            return _repository.GetHealth();
        }
    }
}

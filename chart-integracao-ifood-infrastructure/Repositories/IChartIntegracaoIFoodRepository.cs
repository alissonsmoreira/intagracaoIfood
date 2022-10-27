using chart_integracao_ifood_infrastructure.Models.Common;
using System.Threading.Tasks;

namespace chart_integracao_ifood_infrastructure.Repositories
{
    public interface IChartIntegracaoIFoodRepository
    {
        public Task<Result<string>> GetHealth();
    }
}

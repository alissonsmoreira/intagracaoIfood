using chart_integracao_ifood_infrastructure.Models.Common;
using System.Threading.Tasks;

namespace chart_integracao_ifood_infrastructure.Services
{
    public interface IChartIntegracaoIFoodService
    {
        public Task<Result<string>> GetHealth();
    }
}

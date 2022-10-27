using chart_integracao_ifood_infrastructure.Entities;
using chart_integracao_ifood_infrastructure.Models.Common;

namespace chart_integracao_ifood_infrastructure.Services
{
    public interface IOrderReadyToPickUpService
    {
        Result Proccess(Events events);
    }
}
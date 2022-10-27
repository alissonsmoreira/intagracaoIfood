using chart_integracao_ifood_infrastructure.Entities;
using chart_integracao_ifood_infrastructure.Models.Common;
using chart_integracao_ifood_infrastructure.Repositories;
using chart_integracao_ifood_infrastructure.Services;
using System.Collections.Generic;
using System.Linq;

namespace chart_integracao_ifood_business.Services
{
    public class OrderReadyToPickUpService : IOrderReadyToPickUpService
    {
        private readonly IIFoodRepository _iFoodRepository;
        private readonly IPDVRepository _pdvRepository;

        public OrderReadyToPickUpService(IIFoodRepository iFoodRepository, IPDVRepository pdvRepository)
        {
            _iFoodRepository = iFoodRepository;
            _pdvRepository = pdvRepository;
        }

        public Result Proccess(Events events)
        {
            return events.Code switch
            {
                "RTP" => OrderReadyToPickUp(events.OrderId),
                _ => Result.Erro("Evento inválido"),
            };
        }

        private Result OrderReadyToPickUp(string orderId)
        {
            return _pdvRepository.OrderReadyToPickUp(orderId);
        }
    }
}

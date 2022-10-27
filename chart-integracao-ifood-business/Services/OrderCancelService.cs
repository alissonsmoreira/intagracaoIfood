using chart_integracao_ifood_infrastructure.Entities;
using chart_integracao_ifood_infrastructure.Models.Common;
using chart_integracao_ifood_infrastructure.Repositories;
using chart_integracao_ifood_infrastructure.Services;
using System.Collections.Generic;
using System.Linq;

namespace chart_integracao_ifood_business.Services
{
    public class OrderCancelService : IOrderCancelService
    {
        private readonly IPDVRepository _pdvRepository;

        public OrderCancelService(IPDVRepository pdvRepository)
        {
            _pdvRepository = pdvRepository;
        }

        public Result Proccess(Events events)
        {
            return events.Code switch
            {
                "CAN" => CancellationRequested(events.OrderId),
                "CON" => CancelarionRequestedFailed(events.OrderId),
                "DSP" => ConsumerCancelarionRequested(events.OrderId),
                "CFM" => ConsumerCancelarionAccepted(events.OrderId),
                "RTP" => ConsumerCancelarionDenied(events.OrderId),
                _ => Result.Erro("Evento inválido"),
            };
        }

        private Result CancellationRequested(string orderId)
        {
            return _pdvRepository.CancellationRequested(orderId);
        }
        private Result CancelarionRequestedFailed(string orderId)
        {
            return _pdvRepository.CancelarionRequestedFailed(orderId);
        }
        private Result ConsumerCancelarionRequested(string orderId)
        {
            return _pdvRepository.ConsumerCancelationRequested(orderId);
        }
        private Result ConsumerCancelarionAccepted(string orderId)
        {
            return _pdvRepository.ConsumerCancelationAccepted(orderId);
        }
        private Result ConsumerCancelarionDenied(string orderId)
        {
            return _pdvRepository.ConsumerCancelationDenied(orderId);
        }
    }
}

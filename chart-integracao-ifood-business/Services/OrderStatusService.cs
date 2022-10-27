using chart_integracao_ifood_infrastructure.Entities;
using chart_integracao_ifood_infrastructure.Models.Common;
using chart_integracao_ifood_infrastructure.Repositories;
using chart_integracao_ifood_infrastructure.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace chart_integracao_ifood_business.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IIFoodRepository _iFoodRepository;
        private readonly IPDVRepository _pdvRepository;
        private readonly IEventsRepository _eventRepository;

        public OrderStatusService(IIFoodRepository iFoodRepository, IPDVRepository pdvRepository, IEventsRepository eventsRepository)
        {
            _iFoodRepository = iFoodRepository;
            _pdvRepository = pdvRepository;
            _eventRepository = eventsRepository;
        }

        public Result Proccess(Events events)
        {
            return events.Code switch
            {
                "PLC" => OrderPlaced(events),
                "CFM" => OrderConfirmed(events.OrderId),
                "DSP" => OrderDispached(events.OrderId),
                "CON" => OrderConcluded(events.OrderId),
                "RTP" => OrderReadyToPickUp(events.OrderId),
                "CAN" => OrderCanceled(events.OrderId),
                _ => Result.Erro("Evento inválido"),
            };
        }
        private Result OrderPlaced(Events events)
        {
            Result<OrderDetails> result = _iFoodRepository.GetOrderDetail(events.OrderId);
            List<Events> eventList = new List<Events>();
            if (result.Success)
            {
                OrderDetails details = result.Content;

                var options = new JsonSerializerOptions
                                {
                                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                                    WriteIndented = true
                                };

                events.Payload = JsonSerializer.Serialize(details, options);
                eventList.Add(events);  
                return _eventRepository.UpdateEvents(eventList);
            }

            return Result.Erro("Falha ao obter detalhes do pedido");
        }
        private Result OrderConfirmed(string orderId)
        {
            return _pdvRepository.ConfirmOrder();
        }
        private Result OrderDispached(string orderId)
        {
            return _pdvRepository.OrderDispached(orderId);
        }
        private Result OrderConcluded(string orderId)
        {
            return _pdvRepository.OrderConcluded(orderId);
        }
        private Result OrderReadyToPickUp(string orderId)
        {
            return _pdvRepository.OrderReadyToPickUp(orderId);
        }
        private Result OrderCanceled(string orderId)
        {
            return _pdvRepository.OrderCanceled(orderId);
        }

    }
}

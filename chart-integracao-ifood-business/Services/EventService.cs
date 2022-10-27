using chart_integracao_ifood_infrastructure.Entities;
using chart_integracao_ifood_infrastructure.Models.Common;
using chart_integracao_ifood_infrastructure.Repositories;
using chart_integracao_ifood_infrastructure.Services;
using System.Collections.Generic;
using System.Linq;

namespace chart_integracao_ifood_business.Services
{
    public class EventService : IEventService
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IIFoodRepository _iFoodRepository;
        private readonly IOrderStatusService _orderStatusService;
        private readonly IOrderReadyToPickUpService _orderReadyToPickUpService;
        private readonly IOrderCancelService _orderCancelService;

        public EventService(IEventsRepository eventsRepository, IIFoodRepository iFoodRepository, IOrderStatusService orderStatusService, IOrderReadyToPickUpService orderReadyToPickUpService, IOrderCancelService orderCancelService)
        {
            _eventsRepository = eventsRepository;
            _iFoodRepository = iFoodRepository;
            _orderStatusService = orderStatusService;
            _orderReadyToPickUpService = orderReadyToPickUpService;
            _orderCancelService = orderCancelService;
        }
        public void GetNewEvents()
        {
            var newEvents = _iFoodRepository.GetNewEvents();

            foreach (var item in newEvents)
            {
                var exist = _eventsRepository.Get(item.Id);

                if (!exist.Success)
                {
                    _eventsRepository.Save(item);
                }
            }
        }
        public void AcknowledgmentEvents()
        {
            var noAknowEvents = _eventsRepository.GetUnknownEvents().Content;
            if (noAknowEvents != null)
            {
                EventsIds[] Ids = noAknowEvents.Select(x => new EventsIds() { Id = x.Id }).ToArray();

                if (_iFoodRepository.AcknowledgmentEvents(Ids).Success)
                {
                    foreach (Events evento in noAknowEvents)
                    {
                        evento.Acknowledged = true;
                    }
                    _eventsRepository.UpdateEvents(noAknowEvents);
                }
            }
        }
        public Result<List<EventsToSend>> GetAcknowledgmentEvents() 
        {
            var events = _eventsRepository.GetProcessedEvents();

            List<EventsToSend> eventList = new List<EventsToSend>();

            if(events.Content != null && events.Content.Any())
            {
                foreach (var evento in events.Content)
                {
                    EventsToSend eventTosend = new EventsToSend()
                    {
                        Id = evento.Id,
                        CreatedAt = evento.CreatedAt,
                        FullCode = evento.FullCode,
                        OrderId = evento.OrderId,
                        Payload = evento.Payload,

                    };
                    eventList.Add(eventTosend); 
                }

              return Result<List<EventsToSend>>.Ok(eventList);
            }
            return Result<List<EventsToSend>>.Erro("Não há pedido.");
        }
        public Result UpdateSendedEvents(string[] eventsIds)
        {
            var events = GetEventsById(eventsIds).Content;
           
            if(events != null && events.Any())
            {
                foreach (var evento in events)
                {
                    evento.Sended = true;
                }
                return _eventsRepository.UpdateEvents(events);
            }
            else
            {
                return Result.Erro("Erro ao buscar evento.");
            }
            
        }
        public Result<IEnumerable<Events>> GetEventsById(string[] eventsIds)
        {
            return _eventsRepository.GetEventsById(eventsIds); 
        }

        public Result PurgeEvents()
        {
            var events = _eventsRepository.GetEventsOlderThan(14).Content;
            if(events != null)
            {
                return _eventsRepository.PurgeEvents(events);
            }
            else
            {
                return Result.Erro("Não há eventos para ser expurgado.");
            }  
        }

        public void ProccessEvents()
        {
            var unprocessedEvents = _eventsRepository.GetUnprocessedEvents().Content;

            if (unprocessedEvents != null)
            {
                foreach (Events events in unprocessedEvents)
                {
                    Result resultado;
                    switch (events.Code)
                    {
                        case "PLC":
                        case "CFM":
                        case "DSP":
                        case "CON":
                        case "RTP":
                        case "CAN":
                            resultado = _orderStatusService.Proccess(events);
                            break;
                        case "CAR":
                        case "CARF":
                        case "CCR":
                        case "CCA":
                        case "CCD":
                            resultado = _orderCancelService.Proccess(events);
                            break;
                        case "PAA":
                            resultado = _orderReadyToPickUpService.Proccess(events);
                            break;
                        default:
                            resultado = Result.Ok();
                            break;
                    }

                    if (resultado.Success)
                    {
                        _eventsRepository.UpdateUnprocessedEvent(events.Id);
                    }
                }
            }
        }
    }
}

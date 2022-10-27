using chart_integracao_ifood_infrastructure.Entities;
using chart_integracao_ifood_infrastructure.Models.Common;
using System.Collections.Generic;

namespace chart_integracao_ifood_infrastructure.Services
{
    public interface IEventService
    {
        void GetNewEvents();
        void AcknowledgmentEvents();
        void ProccessEvents();
        Result PurgeEvents();
        Result UpdateSendedEvents(string[] eventsIds);
        Result<List<EventsToSend>> GetAcknowledgmentEvents();
        Result<IEnumerable<Events>> GetEventsById(string[] eventsIds);
    }
}

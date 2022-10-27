using chart_integracao_ifood_infrastructure.Entities;
using chart_integracao_ifood_infrastructure.Models.Common;
using System.Collections.Generic;
using System.Linq;

namespace chart_integracao_ifood_infrastructure.Repositories
{
    public interface IEventsRepository
    {
        Result<Events> Get(string id);
        Result<Events> Save(Events events);
        Result<IEnumerable<Events>> GetUnknownEvents();
        Result UpdateEvents(IEnumerable<Events> events);
        Result<IEnumerable<Events>> GetUnprocessedEvents();
        Result UpdateUnprocessedEvent(string orderId);
        Result PurgeEvents(IEnumerable<Events> events);
        Result<IEnumerable<Events>> GetEventsOlderThan(int days);
        Result<IEnumerable<Events>> GetProcessedEvents();
        Result<IEnumerable<Events>> GetEventsById(string[] eventsIds);
    }
}

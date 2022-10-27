using chart_integracao_ifood_infrastructure.Entities;
using chart_integracao_ifood_infrastructure.Models.Common;
using System.Collections.Generic;

namespace chart_integracao_ifood_infrastructure.Repositories
{
    public interface IIFoodRepository
    {
        IEnumerable<Events> GetNewEvents();
        Result AcknowledgmentEvents(EventsIds[] Ids);
        Result<OrderDetails> GetOrderDetail(string orderId);
    }
}

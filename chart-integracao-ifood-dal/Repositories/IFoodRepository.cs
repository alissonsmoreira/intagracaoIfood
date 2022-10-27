using chart_integracao_ifood_infrastructure.Entities;
using chart_integracao_ifood_infrastructure.Gateways;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using chart_integracao_ifood_infrastructure.Models.Common;
using Microsoft.Extensions.Configuration;

namespace chart_integracao_ifood_infrastructure.Repositories
{
    public class IFoodRepository : IIFoodRepository
    {
        private readonly IIFoodGateway _gateway;

        public IFoodRepository(IIFoodGateway gateway)
        {
            _gateway = gateway;

        }

        public IEnumerable<Events> GetNewEvents()
        {
            var response = _gateway.GetNewEvents().Result;

            if (response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.NoContent)
            {
                return response.Content;
            }

            return Enumerable.Empty<Events>();
        }

        public Result AcknowledgmentEvents(EventsIds[] Ids)
        {
            var response = _gateway.EventsAcknow(Ids).Result;

            if (response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.NoContent)
            {
                return Result.Ok();
            }
            else
            {
                return Result.Erro("Erro ao atribuir Acknowledgment no evento");
            }
        }

        public Result<OrderDetails> GetOrderDetail(string orderId)
        {
            var response = _gateway.GetOrderDetail(orderId).Result;
            OrderDetails details = response.Content;

            if (response.IsSuccessStatusCode && response.StatusCode != HttpStatusCode.NoContent)
            {
                return Result<OrderDetails>.Ok(details);
            }
            else
            {
                return Result<OrderDetails>.Erro("Erro ao obter detalhes do pedido");
            }
        }
        public Result CreateNewOrder(OrderDetails details)
        {
            return Result.Ok();
        }
    }
}

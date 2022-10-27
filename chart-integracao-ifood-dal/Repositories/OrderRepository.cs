using chart_integracao_ifood_infrastructure.Gateways;
using chart_integracao_ifood_infrastructure.Models;
using chart_integracao_ifood_infrastructure.Models.Common;
using chart_integracao_ifood_infrastructure.Repositories;

namespace chart_integracao_ifood_dal.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        IIFoodGateway _gateway;
        private string orderID = "440fb1f3-d6fc-4ab0-b7cf-05197e2ac6ed";
        
        public OrderRepository(IIFoodGateway gateway)
        {
            _gateway = gateway;
        }
        public Result ConfirmOrder(string orderId)
        {
            var response = _gateway.ConfirmOrder(orderId).Result;

            return response.IsSuccessStatusCode ? Result.Ok() : Result<string>.Erro(response.Error.Content);
        }
        public Result OrderReadyToPickUp(string orderId)
        {
            var response = _gateway.OrderReadyToPickUp(orderId).Result;
            
            return response.IsSuccessStatusCode ? Result.Ok() : Result<string>.Erro(response.Error.Content);       
        }
        public Result OrderDispatch(string orderId)
        {
            var response = _gateway.OrderDispatch(orderId).Result;

            return response.IsSuccessStatusCode ? Result.Ok() : Result<string>.Erro(response.Error.Content);
        }
        public Result OrderRequestCancellation(string orderId, CancelattionRequestBody cancelation)
        {
            var response = _gateway.OrderRequestCancellation(orderId, cancelation).Result;

            return response.IsSuccessStatusCode ? Result.Ok() : Result<string>.Erro(response.Error.Content);
        }
        public Result OrderAcceptCancellation(string orderId)
        {
            var response = _gateway.OrderAcceptCancellation(orderID).Result;

            return response.IsSuccessStatusCode ? Result.Ok() : Result<string>.Erro(response.Error.Content);
        }
        public Result OrderDenyCancellation(string orderId)
        {
            var response = _gateway.OrderDenyCancellation(orderID).Result;

            return response.IsSuccessStatusCode ? Result.Ok() : Result<string>.Erro(response.Error.Content);
        }
    }
}

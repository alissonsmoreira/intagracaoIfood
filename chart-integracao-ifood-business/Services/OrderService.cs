using chart_integracao_ifood_infrastructure.Models;
using chart_integracao_ifood_infrastructure.Models.Common;
using chart_integracao_ifood_infrastructure.Repositories;
using chart_integracao_ifood_infrastructure.Services;
using System.Timers;

namespace chart_integracao_ifood_business.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public Result ConfirmOrder(string orderId)
        {

            return _orderRepository.ConfirmOrder(orderId);
        }
        public Result OrderReadyToPickUp(string orderId)
        {
            return _orderRepository.OrderReadyToPickUp(orderId);
        }
        public Result OrderDispatch(string orderId)
        {
            return _orderRepository.OrderDispatch(orderId);
        }
        public Result OrderRequestCancellation(string orderId, CancelattionRequestBody cancelation)
        {
            return _orderRepository.OrderRequestCancellation(orderId, cancelation);
        }
        public Result OrderAcceptCancellation(string orderId)
        {
            return _orderRepository.OrderAcceptCancellation(orderId);
        }
        public Result OrderDenyCancellation(string orderId)
        {
            return _orderRepository.OrderDenyCancellation(orderId);
        }
    }
}

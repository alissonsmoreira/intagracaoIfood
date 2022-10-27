using chart_integracao_ifood_infrastructure.Entities;
using chart_integracao_ifood_infrastructure.Models;
using chart_integracao_ifood_infrastructure.Models.Common;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace chart_integracao_ifood_infrastructure.Gateways
{
    public interface IIFoodGateway
    {
        [Get("/order/v1.0/events:polling")]
        Task<ApiResponse<IEnumerable<Events>>> GetNewEvents();

        [Post("/order/v1.0/events/acknowledgment")]
        Task<ApiResponse<IEnumerable<Events>>> EventsAcknow([Body] EventsIds[] Ids);

        [Get("/order/v1.0/orders/{orderId}")]
        Task<ApiResponse<OrderDetails>> GetOrderDetail(string orderId);

        [Post("/order/v1.0/orders/{orderId}/confirm")]
        Task<ApiResponse<IEnumerable<Events>>> ConfirmOrder(string orderId);

        [Post("/order/v1.0/orders/{orderId}/readyToPickup")]
        Task<ApiResponse<IEnumerable<Events>>> OrderReadyToPickUp(string orderId);

        [Post("/order/v1.0/orders/{orderId}/dispatch")]
        Task<ApiResponse<IEnumerable<Events>>> OrderDispatch(string orderId);

        [Post("/order/v1.0/orders/{orderId}/requestCancellation")]
        Task<ApiResponse<IEnumerable<Events>>> OrderRequestCancellation(string orderId, [Body] CancelattionRequestBody cancelation);
        
        [Post("/order/v1.0/orders/{orderId}/acceptCancellation")]
        Task<ApiResponse<IEnumerable<Events>>> OrderAcceptCancellation(string orderId);

        [Post("/order/v1.0/orders/{orderId}/denyCancellation")]
        Task<ApiResponse<IEnumerable<Events>>> OrderDenyCancellation(string orderId);


    }
}

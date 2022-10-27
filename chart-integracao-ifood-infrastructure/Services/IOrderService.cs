using chart_integracao_ifood_infrastructure.Models;
using chart_integracao_ifood_infrastructure.Models.Common;

namespace chart_integracao_ifood_infrastructure.Services
{
    public interface IOrderService
    {
        Result ConfirmOrder(string orderId);
        Result OrderReadyToPickUp(string orderId);
        Result OrderDispatch(string orderId);
        Result OrderRequestCancellation(string orderId, CancelattionRequestBody cancelation);
        Result OrderAcceptCancellation(string orderId);
        Result OrderDenyCancellation(string orderId);

    }
}

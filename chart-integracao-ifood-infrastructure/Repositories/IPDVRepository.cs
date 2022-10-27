using chart_integracao_ifood_infrastructure.Entities;
using chart_integracao_ifood_infrastructure.Models.Common;
using System.Collections.Generic;

namespace chart_integracao_ifood_infrastructure.Repositories
{
    public interface IPDVRepository
    {
        Result ConfirmOrder();
        Result CreateNewOrder(OrderDetails details);
        Result OrderDispached(string orderId);
        Result OrderConcluded(string orderId);
        Result OrderCanceled(string orderId);
        Result OrderReadyToPickUp(string orderId);
        Result CancellationRequested(string orderId);
        Result CancelarionRequestedFailed(string orderId);
        Result ConsumerCancelationRequested(string orderId);
        Result ConsumerCancelationAccepted(string orderId);
        Result ConsumerCancelationDenied(string orderId);

    }
}

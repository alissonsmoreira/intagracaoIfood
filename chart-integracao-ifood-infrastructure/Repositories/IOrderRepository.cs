using chart_integracao_ifood_infrastructure.Models;
using chart_integracao_ifood_infrastructure.Models.Common;
using System.Threading.Tasks;

namespace chart_integracao_ifood_infrastructure.Repositories
{
    public interface IOrderRepository
    {
         Result ConfirmOrder(string orderId);
         Result OrderReadyToPickUp(string orderId);
         Result OrderDispatch(string orderId);
         Result OrderRequestCancellation(string orderId, CancelattionRequestBody cancelation);
         Result OrderAcceptCancellation(string orderId);
         Result OrderDenyCancellation(string orderId);

    }
}

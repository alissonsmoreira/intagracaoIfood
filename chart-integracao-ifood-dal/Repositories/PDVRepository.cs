using chart_integracao_ifood_infrastructure.Entities;
using chart_integracao_ifood_infrastructure.Gateways;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using chart_integracao_ifood_infrastructure.Models.Common;

namespace chart_integracao_ifood_infrastructure.Repositories
{
    public class PDVRepository : IPDVRepository
    {
        public Result ConfirmOrder()
        {
            return Result.Ok();
        }
        public Result CreateNewOrder(OrderDetails details)
        {
            return Result.Ok();
        }
        public Result OrderDispached(string orderId)
        {
            return Result.Ok();
        }
        public Result OrderConcluded(string orderId)
        {
            return Result.Ok();
        }
        public Result OrderCanceled(string orderId)
        {
            return Result.Ok();
        }
        public Result OrderReadyToPickUp(string orderId)
        {
            return Result.Ok();
        }
        public Result CancellationRequested(string orderId)
        {
            return Result.Ok();
        }
        public Result CancelarionRequestedFailed(string orderId)
        {
            return Result.Ok();
        }
        public Result ConsumerCancelationRequested(string orderId)
        {
            return Result.Ok();
        }
        public Result ConsumerCancelationAccepted(string orderId)
        {
            return Result.Ok();
        }
        public Result ConsumerCancelationDenied(string orderId)
        {
            return Result.Ok();
        }
    }
}

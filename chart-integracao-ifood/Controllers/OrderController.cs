using chart_integracao_ifood_infrastructure.Entities;
using chart_integracao_ifood_infrastructure.Models;
using chart_integracao_ifood_infrastructure.Models.Common;
using chart_integracao_ifood_infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace chart_integracao_ifood.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase 
    {
        private IOrderService _orderService;
        private IEventService _eventService;
        public OrderController(IOrderService orderService, IEventService eventService)
        {
            _orderService = orderService;
            _eventService = eventService;
        }

        [HttpGet("GetNewEvents")]
        public Result<List<EventsToSend>> GetNewEvents()
        {
            return _eventService.GetAcknowledgmentEvents();
        }

        [HttpPost("UpdadeSendedEvents")]
        public Result UpdadeSendedEvents([FromBody] string[] eventsIds)
        {
            return _eventService.UpdateSendedEvents(eventsIds);
        }

        [HttpPost("{orderId}/confirm")]
        public Result ConfirmOrder(string orderId)
        {
            return _orderService.ConfirmOrder(orderId);
        }

        [HttpPost("{orderId}/readyToPickup")]
        public Result OrderReadyToPickUp(string orderId)
        {
            return _orderService.OrderReadyToPickUp(orderId);
        }

        [HttpPost("{orderId}/dispatch")]
        public Result OrderDispatch(string orderId)
        {
            return _orderService.OrderDispatch(orderId);
        }

        [HttpPost("{orderId}/requestCancellation")]
        public Result OrderRequestCancellation(string orderId, [FromBody] CancelattionRequestBody cancelation)
        {
            return _orderService.OrderRequestCancellation(orderId, cancelation);
        }

        [HttpPost("{orderId}/acceptCancellation")]
        public Result OrderAcceptCancellation(string orderId)
        {
            return _orderService.OrderAcceptCancellation(orderId);
        }

        [HttpPost("{orderId}/denyCancellation")]
        public Result OrderDenyCancellation(string orderId)
        {
            return _orderService.OrderDenyCancellation(orderId);
        }
    }
}

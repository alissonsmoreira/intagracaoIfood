using System;
using System.Collections.Generic;

namespace chart_integracao_ifood_infrastructure.Entities
{
    public class EventsToSend
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string FullCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Payload { get; set; }
    }
}
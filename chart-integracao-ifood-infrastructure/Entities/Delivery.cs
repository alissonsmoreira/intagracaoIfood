using System;
using System.Collections.Generic;

namespace chart_integracao_ifood_infrastructure.Entities
{
    public class Delivery
    {
        public string mode { get; set; }
        public string deliveredBy { get; set; }
        public DeliveryAddress deliveryAddress { get; set; }
        public DateTime deliveryDateTime { get; set; }
    }
}
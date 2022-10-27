using System;
using System.Collections.Generic;

namespace chart_integracao_ifood_infrastructure.Entities
{
    public class OrderDetails
    {
        public Benefits[] benefits { get; set; }
        public string orderType { get; set; }
        public Payments payments { get; set; }
        public Merchant merchant { get; set; }
        public string salesChannel { get; set; }
        public Picking picking { get; set; }
        public string orderTiming { get; set; }
        public DateTime createdAt { get; set; }
        public Total total { get; set; }
        public DateTime preparationStartDateTime { get; set; }
        public string id { get; set; }
        public string displayId { get; set; }
        public Items[] items { get; set; }
        public Customer customer { get; set; }
        public string extraInfo { get; set; }
        public AdditionalFees[] additionalFees { get; set; }
        public Delivery delivery { get; set; }
        public Schedule schedule { get; set; }
        public Indoor indoor { get; set; }
        public TakeOut takeOut { get; set; }

    }
}
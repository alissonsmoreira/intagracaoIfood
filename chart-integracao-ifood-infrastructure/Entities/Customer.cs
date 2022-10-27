using System;
using System.Collections.Generic;

namespace chart_integracao_ifood_infrastructure.Entities
{
    public class Customer
    {
        public Phone phone { get; set; }
        public string documentNumber { get; set; }
        public string name { get; set; }
        public int ordersCountOnMerchant { get; set; }
        public string id { get; set; }

    }
}
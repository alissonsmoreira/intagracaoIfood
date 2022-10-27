using System;
using System.Collections.Generic;

namespace chart_integracao_ifood_infrastructure.Entities
{
    public class Total
    {
        public decimal benefits { get; set; }
        public decimal deliveryFee { get; set; }
        public decimal orderAmount { get; set; }
        public decimal subTotal { get; set; }
        public decimal additionalFees { get; set; }

    }
}
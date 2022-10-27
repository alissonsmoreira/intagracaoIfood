using System;
using System.Collections.Generic;

namespace chart_integracao_ifood_infrastructure.Entities
{
    public class Payments
    {
        public Methods[] methods { get; set; }
        public decimal pending { get; set; }
        public decimal prepaid { get; set; }
    }
}
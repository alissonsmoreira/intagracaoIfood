using System;
using System.Collections.Generic;

namespace chart_integracao_ifood_infrastructure.Entities
{
    public class Benefits
    {
        public string targetId { get; set; }
        public SponsorShipValues[] SponsorShipValues { get; set; }
        public decimal value { get; set; }
        public string target { get; set; }
    }
}
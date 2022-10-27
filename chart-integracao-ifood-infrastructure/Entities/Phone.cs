using System;
using System.Collections.Generic;

namespace chart_integracao_ifood_infrastructure.Entities
{
    public class Phone
    {
        public string number { get; set; }
        public string localizer { get; set; }
        public DateTime localizerExpiration { get; set; }

    }
}
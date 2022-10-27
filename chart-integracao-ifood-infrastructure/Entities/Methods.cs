using System;
using System.Collections.Generic;

namespace chart_integracao_ifood_infrastructure.Entities
{
    public class Methods
    {
        public Wallet wallet { get; set; }
        public string method { get; set; }
        public bool prepaid { get; set; }
        public string currency { get; set; }
        public string type { get; set; }
        public decimal value { get; set; }
        public Cash cash { get; set; }
        public Card card { get; set; }

    }
}
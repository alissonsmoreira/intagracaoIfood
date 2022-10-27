using System;
using System.Collections.Generic;

namespace chart_integracao_ifood_infrastructure.Entities
{
    public class Options
    {
        public decimal unitPrice { get; set; }
        public string unit { get; set; }
        public string ean { get; set; }
        public int quantity { get; set; }
        public string externalCode { get; set; }
        public decimal price { get; set; }
        public string name { get; set; }
        public int index { get; set; }
        public string id { get; set; }
        public decimal addition { get; set; }
    }
}
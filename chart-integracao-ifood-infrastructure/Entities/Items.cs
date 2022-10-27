using System;
using System.Collections.Generic;

namespace chart_integracao_ifood_infrastructure.Entities
{
    public class Items
    {
        public decimal unitPrice { get; set; }
        public int quantity { get; set; }
        public string externalCode { get; set; }
        public decimal totalPrice { get; set; }
        public int index { get; set; }
        public string unit { get; set; }
        public string ean { get; set; }
        public decimal price { get; set; }
        public string observations { get; set; }
        public string imageUrl { get; set; }
        public string name { get; set; }
        public Options[] options { get; set; }
        public string id { get; set; }
        public decimal optionsPrice { get; set; }

    }
}
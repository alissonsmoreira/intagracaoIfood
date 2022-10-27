using System;
using System.Collections.Generic;

namespace chart_integracao_ifood_infrastructure.Entities
{
    public class DeliveryAddress
    {
        public string reference { get; set; }
        public string country { get; set; }
        public string streetName { get; set; }
        public string formattedAddress { get; set; }
        public string streetNumber { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public Coordinates coordinates { get; set; }
        public string neighborhood { get; set; }
        public string state { get; set; }
        public string complement { get; set; }
    }
}
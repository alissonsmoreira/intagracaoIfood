using chart_integracao_ifood_infrastructure.Enums;
using System;

namespace chart_integracao_ifood_infrastructure.Models
{
    public class CancelattionRequestBody
    {
        public string reason { get; set; }
        public string cancellationCode { get; set; }

    }
}
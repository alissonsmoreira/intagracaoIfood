using System;

namespace chart_integracao_ifood_infrastructure.Models
{
    public class AccessToken
    {
        public string accessToken { get; set; }
        public string type { get; set; }
        public int expiresIn { get; set; }
        public DateTime created { get; set; }
    }
}
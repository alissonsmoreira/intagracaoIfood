using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chart_integracao_ifood_infrastructure.Enums
{
    public enum CancelationCode
    {
        [Description("Problemas de sistema")]
        SystemProblem = 501,

        [Description("Pedido em duplicidade")]
        DuplicateRequest = 502,

        [Description("Item indisponível")]
        UnavailableItem = 503,
            
        [Description("Restaurante sem motoboy")]
        NoMotoboyAvailable = 504,

        [Description("Cardápio desatualizado")]
        OutdatedMenu = 505,
            
        [Description("Pedido fora da área de entrega")]
        OutsideDeliveryArea = 506,
            
        [Description("Cliente golpista / Trote")]
        ScamCliente = 507,
            
        [Description("Pedido fora do horário de entrega")]
        OutsideDeliverySchedule = 508,
            
        [Description("Problemas internos do restaurante")]
        RestaurantInternalProblem = 509,
            
        [Description("Área de risco")]
        DangerousArea = 511,
            
        [Description("Restaurante abrirá mais tarde")]
        RestaurantWillOpenLater = 512,
            
        [Description("Restaurante fechou mais cedo")]
        RestaurantClosedEarly = 513,

    }
}

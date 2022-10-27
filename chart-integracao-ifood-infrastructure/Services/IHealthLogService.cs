using System.Collections.Generic;

namespace chart_integracao_ifood_infrastructure.Services
{
    public interface IHealthLogService
    {
        List<string> GetHealthLogs();
        void AddHealthLog(string log);
        void RemoveHealthLog(string log);
    }
}

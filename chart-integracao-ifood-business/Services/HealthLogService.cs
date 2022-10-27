using chart_integracao_ifood_infrastructure.Services;
using System.Collections.Generic;

namespace chart_integracao_ifood_business.Services
{
    public class HealthLogService : IHealthLogService
    {
        private List<string> _logs;

        public HealthLogService()
        {
            _logs = new List<string>();
        }

        public void AddHealthLog(string log)
        {
            _logs.Add(log);
        }

        public List<string> GetHealthLogs()
        {
            return _logs;
        }

        public void RemoveHealthLog(string log)
        {
            _logs.RemoveAll(x => x == log);
        }
    }
}

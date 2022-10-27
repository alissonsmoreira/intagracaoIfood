using chart_integracao_ifood_infrastructure.Constants;
using chart_integracao_ifood_infrastructure.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace chart_integracao_ifood.HealthChecks
{
    public class ApiIFoodHealthCheck : IHealthCheck
    {
        private readonly IHealthLogService _healthLogService;

        public ApiIFoodHealthCheck(IHealthLogService healthLogService)
        {
            _healthLogService = healthLogService;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var existeFalhaIfood = _healthLogService.GetHealthLogs().Any(x => x == HealthLogCodes.IFOOD_UNAUTHORIZED);

            if(existeFalhaIfood)
                return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, "Falha na autenticação com o IFood"));

            var existeFalhaBanco = _healthLogService.GetHealthLogs().Any(x => x == HealthLogCodes.DATABASE_CONNECTION_ERROR);

            return Task.FromResult(existeFalhaBanco ? new HealthCheckResult(context.Registration.FailureStatus, "Falha na conexão com o banco de dados") : HealthCheckResult.Healthy());
        }
    }
}

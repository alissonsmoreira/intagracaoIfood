using chart_integracao_ifood_infrastructure.Gateways;
using chart_integracao_ifood_infrastructure.Models.Common;
using Refit;

namespace chart_integracao_ifood_tray
{
    public class ServiceCheck
    {
        private IChartIntegracaoIfoodGateway _gateway;

        public ServiceCheck(int port)
        {
            _gateway = RestService.For<IChartIntegracaoIfoodGateway>($"http://localhost:{port}");
        }

        public Result<string> GetHealth()
        {
            var responseTask = _gateway.Health();

            try
            {
                var response = responseTask.Result;

                return response.IsSuccessStatusCode ? Result<string>.Ok(response.Content) : Result<string>.Erro(response.Error.Content);
            }
            catch
            {
                return Result<string>.Erro("Serviço indisponível");
            }
        }
    }
}

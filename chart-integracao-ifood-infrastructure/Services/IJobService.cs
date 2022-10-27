namespace chart_integracao_ifood_infrastructure.Services
{
    public interface IJobService
    {
        void StartEventTimer();
        void StartAcknowledgmentTimer();
        void StartTypeOrderSelectorTimer();
        void StartPurgOrderSelectorTimer();
    }
}
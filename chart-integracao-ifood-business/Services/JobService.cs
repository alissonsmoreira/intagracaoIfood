using chart_integracao_ifood_infrastructure.Services;
using System;
using System.Timers;

namespace chart_integracao_ifood_business.Services
{
    public class JobService : IJobService
    {
        private const int EVENT_TIME = 30000;
        private const int ACKNOWLEDGMENT_TIME = 30000;
        private const int TYPE_ORDER_SELECTOR = 30000;
        private const int PURGE_ORDER_TIME = 7200000;
        private readonly IServiceProvider _serviceProvider;

        public JobService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void StartEventTimer()
        {
            GetService().GetNewEvents();
            Timer timerMatching = new(EVENT_TIME)
            {
                Enabled = true
            };
            timerMatching.Elapsed += new ElapsedEventHandler(StartEventTimerElapsed);
            timerMatching.Start();
        }
        private void StartEventTimerElapsed(object sender, ElapsedEventArgs e)
        {
            GetService().GetNewEvents();
        }
        public void StartAcknowledgmentTimer()
        {
            GetService().AcknowledgmentEvents();
            Timer timerMatching = new(ACKNOWLEDGMENT_TIME)
            {
                Enabled = true
            };
            timerMatching.Elapsed += new ElapsedEventHandler(StartAcknowledgmentTimerElapsed);
            timerMatching.Start();
        }
        private void StartAcknowledgmentTimerElapsed(object sender, ElapsedEventArgs e)
        {
            GetService().AcknowledgmentEvents();
        }

        public void StartTypeOrderSelectorTimer()
        {
            GetService().ProccessEvents();
            Timer timerMatching = new(TYPE_ORDER_SELECTOR)
            {
                Enabled = true
            };
            timerMatching.Elapsed += new ElapsedEventHandler(StartTypeOrderSelectorTimerElapsed);
            timerMatching.Start();
        }
        private void StartTypeOrderSelectorTimerElapsed(object sender, ElapsedEventArgs e)
        {
            GetService().ProccessEvents();
        }

        public void StartPurgOrderSelectorTimer()
        {
            GetService().PurgeEvents();
            Timer timerMatching = new(PURGE_ORDER_TIME)
            {
                Enabled = true
            };
            timerMatching.Elapsed += new ElapsedEventHandler(StartPurgOrderSelectorTimerElapsed);
            timerMatching.Start();
        }
        private void StartPurgOrderSelectorTimerElapsed(object sender, ElapsedEventArgs e)
        {
            GetService().PurgeEvents();
        }

        private IEventService GetService()
        {
            return (IEventService)_serviceProvider.GetService(typeof(IEventService));
        }
    }
}

using chart_integracao_ifood_business.Services;
using chart_integracao_ifood_dal.Handlers;
using chart_integracao_ifood_dal.Repositories;
using chart_integracao_ifood_infrastructure.Entities;
using chart_integracao_ifood_infrastructure.Gateways;
using chart_integracao_ifood_infrastructure.Repositories;
using chart_integracao_ifood_infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Net.Http.Headers;

namespace chart_integracao_ifood_dependecy_injection
{
    public static class DependencyInjection
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureServices();
            services.ConfigureRepositores();
            services.ConfigureGateways(configuration);
            services.ConfigureDatabases(configuration);
        }

        private static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IHealthLogService, HealthLogService>();
            services.AddScoped<IChartIntegracaoIFoodService, ChartIntegracaoIFoodService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IJobService, JobService>();
            services.AddTransient<IOrderStatusService, OrderStatusService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderReadyToPickUpService, OrderReadyToPickUpService>();
            services.AddTransient<IOrderCancelService, OrderCancelService>();
        }

        private static void ConfigureRepositores(this IServiceCollection services)
        {
            services.AddScoped<IChartIntegracaoIFoodRepository, ChartIntegracaoIFoodRepository>();
            services.AddTransient<IEventsRepository, EventsRepository>();
            services.AddTransient<IIFoodRepository, IFoodRepository>();
            services.AddTransient<IPDVRepository, PDVRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<AuthHeaderHandler>();
        }

        private static void ConfigureGateways(this IServiceCollection services, IConfiguration configuration)
        {
            var chartIntegracaoIFoodPort = configuration["Port"];
            services
                .AddRefitClient<IChartIntegracaoIfoodGateway>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri($"http://localhost:{chartIntegracaoIFoodPort}"));

            services
                .AddRefitClient<IIFoodAuthGateway>()
                .ConfigureHttpClient((c) =>
                {
                    c.BaseAddress = new Uri($"https://merchant-api.ifood.com.br");
                    c.DefaultRequestHeaders.Clear();
                    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                });

            services
                .AddRefitClient<IIFoodGateway>()
                .ConfigureHttpClient((c) =>
                {
                    c.BaseAddress = new Uri($"https://merchant-api.ifood.com.br");
                    c.DefaultRequestHeaders.Clear();
                    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                })
                .AddHttpMessageHandler<AuthHeaderHandler>();
        }

        private static void ConfigureDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<AppDbContext>((options) =>
            {
                options.UseNpgsql(configuration["ConnectionString"]);
            }, ServiceLifetime.Transient);
        }
    }
}

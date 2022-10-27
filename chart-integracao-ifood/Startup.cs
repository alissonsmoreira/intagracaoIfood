using chart_integracao_ifood.HealthChecks;
using chart_integracao_ifood_dependecy_injection;
using chart_integracao_ifood_infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace chart_integracao_ifood
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddHealthChecks()
                .AddCheck<ApiIFoodHealthCheck>("api_ifood_health_check");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "chart_integracao_ifood", Version = "v1" });
            });

            services.AddMemoryCache();

            services.ConfigureDependencyInjection(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "chart_integracao_ifood v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    ResponseWriter = WriteResponse
                });
                endpoints.MapControllers();
            });

            StartJobs(app);
        }

        private void StartJobs(IApplicationBuilder app)
        {
            var jobService = app.ApplicationServices.GetService<IJobService>();
            
            jobService.StartEventTimer();
            jobService.StartAcknowledgmentTimer();
            jobService.StartTypeOrderSelectorTimer();
            jobService.StartPurgOrderSelectorTimer();
        }

        private static Task WriteResponse(HttpContext context, HealthReport result)
        {
            string message = result.Status.ToString();

            if (result.Entries.Where(x => x.Value.Status != HealthStatus.Healthy).Any())
            {
                var item = result.Entries.FirstOrDefault();

                message = item.Value.Description;
            }

            return context.Response.WriteAsync(message);
        }
    }
}

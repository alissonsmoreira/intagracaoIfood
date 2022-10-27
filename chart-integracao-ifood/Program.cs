using chart_integracao_ifood_infrastructure.Entities.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace chart_integracao_ifood
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            string filePath = GetFilePath();

            string fileName = GetFileName();

            ValidarExistenciaArquivoConfiguracao().Wait();

            if (args.Length > 0 && args[0].Contains("server"))
            {
                System.Console.WriteLine("-------------------- Configuração Server -----------------------------");
                return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
            }
            else
            {
                System.Console.WriteLine("-------------------- Configuração Windows -----------------------------");

                var config = new ConfigurationBuilder()
                    .AddJsonFile($"{filePath}{fileName}", optional: false, reloadOnChange: true)
                    .Build();

                return Host.CreateDefaultBuilder(args)
                        .UseWindowsService()
                        .ConfigureAppConfiguration(configBuilder =>
                        {
                            configBuilder.AddJsonFile($"{filePath}{fileName}", optional: false, reloadOnChange: true);
                        })
                        .ConfigureWebHostDefaults(webBuilder =>
                        {
                            webBuilder.UseStartup<Startup>();
                            webBuilder.UseUrls($"http://localhost:{config.GetValue<string>("Port")}");
                        });
            }
        }

        private static string GetFilePath()
        {
            return $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\\ChartIntegracaoIfood";
        }

        private static string GetFileName()
        {
            return $"\\config.json";
        }

        private static async Task ValidarExistenciaArquivoConfiguracao()
        {
            string filePath = GetFilePath();

            string fileName = GetFileName();

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            if (!File.Exists($"{filePath}{fileName}"))
            {
                var defaultConfig = new Configs()
                {
                    IFoodClientId = string.Empty,
                    IFoodClientSecret = string.Empty
                };

                using FileStream fileStream = File.Create($"{filePath}{fileName}");
                await JsonSerializer.SerializeAsync(fileStream, defaultConfig);
            }
        }
    }
}

using M13.InterviewProject.Interfaces;
using M13.InterviewProject.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace M13.InterviewProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Добавление конфигурации (аналог построения IConfiguration в Startup)
            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            // Добавление сервисов в контейнер DI
            builder.Services.AddControllers();
            builder.Services.AddSingleton<IRuleService, RuleService>();
            builder.Services.AddScoped<IWebPageService, WebPageService>();
            builder.Services.AddScoped<ISpellCheckerService, SpellCheckerService>();

            builder.Services.AddHttpClient("CustomClient")
                .ConfigurePrimaryHttpMessageHandler(() => 
                {
                    return new HttpClientHandler
                    {
                        AllowAutoRedirect = true,
                        AutomaticDecompression = System.Net.DecompressionMethods.Deflate
                    };
                });

            // Логирование
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            var app = builder.Build();

            // Настройка маршрутизации
            app.MapControllers();

            app.Run();
        }
    }  
}
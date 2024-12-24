using M13.InterviewProject.Interfaces;
using M13.InterviewProject.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            builder.Services.AddControllers(); // Используйте AddControllers вместо AddMvc для современных API
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


            builder.Services.AddSingleton<IRulesService, RulesService>();
            builder.Services.AddScoped<IWebPageService, WebPageService>();
            builder.Services.AddScoped<ISpellCheckerService, SpellCheckerService>();

            var app = builder.Build();

            // Настройка middleware
            if (app.Environment.IsDevelopment())
            {
                // Например, можно включить Swagger в режиме разработки
                app.UseDeveloperExceptionPage();
            }

            // Настройка маршрутизации
            app.MapControllers();

            app.Run();
        }
    }  
}
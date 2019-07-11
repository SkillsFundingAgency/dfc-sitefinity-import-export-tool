using System;
using System.IO;
using System.Linq;
using CommandLine;
using Dfc.Sitefinity.ImportExport.Tool.Models;
using Dfc.Sitefinity.ImportExport.Tool.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Dfc.Sitefinity.ImportExport.Tool
{
    class Program
    {
        static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.txt")
                .WriteTo.ColoredConsole()
                .CreateLogger();
            
            Console.WriteLine($"{Directory.GetCurrentDirectory()}");
            
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<WorkflowRunner>>();
            var result = Parser.Default
                .ParseArguments<WorkflowRunner.Options>(args)
                .MapResult(
                    (opts) => WorkflowRunner.Execute(serviceProvider, opts),
                    errs => {
                        logger.LogError(errs.Aggregate("", (s,e) => s += e.ToString() + Environment.NewLine));
                        return SuccessFailCode.Fail;
                    });

            return (int)result;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();
            
            services.AddLogging(l => l.AddSerilog())
                .AddSingleton<SiteFinityHttpService>()
                .AddSingleton<WorkflowRunner>();
                

            services.AddSingleton<IConfiguration>(config);
            services.Configure<SiteFinitySettings>(config.GetSection("SitefinitySettings"));
            
            services.AddTransient<ISiteFinityHttpService, SiteFinityHttpService>();
        }
    }
}

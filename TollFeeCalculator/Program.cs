using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using TollFeeCalculator.console;

namespace TollFeeCalculator
{
   public class Program
    {
        private readonly ILogger<Program> _logger;


        public Program(ILogger<Program> logger,TaxCalculator taxCalculator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }


        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
           
        }

        /// <summary>
        /// build the dependency injection container to inject all the service we need
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.AddHostedService<Worker>();
            services.AddTransient<TaxCalculator>();
           
        });
    }
}

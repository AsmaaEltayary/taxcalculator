using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace TollFeeCalculator.console
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly TaxCalculator _taxCalculator;
        private readonly IHost _host;


        public Worker(ILogger<Worker> logger, IHost host, TaxCalculator taxCalculator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _host = host ?? throw new ArgumentNullException(nameof(host));
            _taxCalculator = taxCalculator ?? throw new ArgumentNullException(nameof(taxCalculator));

        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
			try
			{
                string tollStringdates = "";
                using (var sr = new StreamReader("testData.txt"))
                {
                    tollStringdates = await sr.ReadToEndAsync();
                }
              
                _logger.LogInformation($"test data are . {tollStringdates}");

                if (!String.IsNullOrEmpty(tollStringdates))
                {
                    List<DateTime> tolldates = GetTollDates(tollStringdates);

                    tolldates.Sort((a, b) => a.CompareTo(b));

                    Console.WriteLine("Test data in UTC");
                    foreach (var item in tolldates)
                    {
                        TimeSpan time = item.TimeOfDay;
                        Console.WriteLine(time);
                    }    
                   int result =_taxCalculator.CalulateTotalFeeCost(tolldates);

                    Console.WriteLine($"Total Fee {result}");
                }
                else
                {
                    Console.WriteLine("Test data is empty...");
                }
            }
			catch (Exception ex)
			{
                Console.WriteLine("SomeThing went Wrong...");
                _logger.LogError($"An error ocurred. {ex.Message}");
            }

			finally
			{
                Console.Write("\n\rPress any key to exit!");
                Console.ReadKey();
                await _host.StopAsync(stoppingToken);

            }
        }


        private List<DateTime> GetTollDates(string Dates)
        {
            String[] tollStringDates = Dates.Split(", ");

            List<DateTime> tollDates = new List<DateTime>();

            for (int i = 0; i < tollStringDates.Length; i++)
            {
                tollDates.Add( DateTime.ParseExact(tollStringDates[i], "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture).ToUniversalTime());
              
            }
            return tollDates.ToList();


        }
    }
}

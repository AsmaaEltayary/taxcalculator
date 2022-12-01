using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Globalization;

namespace TollFeeCalculator.Api.Controllers
{
    [ApiController]
    [Route("api/TollFee")]
    public class TaxCalculatorController : ControllerBase
    {

        private readonly ILogger<TaxCalculatorController> _logger;
        private readonly TaxCalculator _taxCalculator;
        public TaxCalculatorController(TaxCalculator taxCalculator, ILogger<TaxCalculatorController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _taxCalculator = taxCalculator ?? throw new ArgumentNullException(nameof(taxCalculator));
        }

        [HttpGet]
        public ActionResult<int> CalculateCongestionTax(string dateTime)
        {
            try
            {
                // converting the string dates from the request to list<datetime>
                List<DateTime> tollDates = new();
              
                tollDates.Add(DateTime.ParseExact(dateTime, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture));

              

                int reslt =  _taxCalculator.CalulateTotalFeeCost( tollDates);

                return Ok(reslt);

            }
            catch (FormatException)
            {
                return BadRequest("VehicleTollDates: is not well formated should be formated as following yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("total")]
        public ActionResult<int> CalculateTotalCongestionTax(string dates)
        {
            try
            {
                // converting the string dates from the request to list<datetime>
                List<DateTime> tollDates = GetTollDates(dates);
            
                int reslt = _taxCalculator.CalulateTotalFeeCost(tollDates);

                return Ok(reslt);

            }
            catch (FormatException)
            {
                return BadRequest("VehicleTollDates: is not well formated should be formated as following yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }



        private List<DateTime> GetTollDates(string Dates)
        {
            String[] tollStringDates = Dates.Split(",");

            List<DateTime> tollDates = new List<DateTime>();

            for (int i = 0; i < tollStringDates.Length; i++)
            {
                tollDates.Add(DateTime.ParseExact(tollStringDates[i].TrimStart(), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture));

            }
            return tollDates.ToList();


        }
    }


}


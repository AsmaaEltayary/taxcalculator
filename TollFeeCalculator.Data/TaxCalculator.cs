using Microsoft.Extensions.Logging;
using System.Globalization;

namespace TollFeeCalculator
{
    public class TaxCalculator
    {
       // private readonly ILogger<TaxCalculator> _logger;

        public CityTollRules TaxRules { get; set; }

        public TaxCalculator()
        {
            TaxRules=GetTaxRulesFromDataSource();
           // _logger = logger;
        }
        /// <summary>
        /// TODO tax rules should be stored in data source (SQL/COUCH) or JsonFile
        /// </summary>
        /// 
        private CityTollRules GetTaxRulesFromDataSource()
        {
            return new CityTollRules
            {

                MaxTollfeePerDay = 60,
                TaxFees = new List<TollFee> {
            new TollFee {
                From= "06:00",
                To= "06:29",
                Fee= 9
            },new TollFee{
                From= "06:30",
                To= "06:59",
                Fee= 14
             },new TollFee{
                From= "07:00",
                To= "07:59",
                Fee= 18
             },new TollFee{
                From= "08:00",
                To= "08:29",
                Fee= 14
             },new TollFee{
                From= "08:30",
                To= "14:59",
                Fee= 9
             },new TollFee{
                From= "15:00",
                To= "15:29",
                Fee= 14
             },new TollFee{
                From= "15:30",
                To= "16:59",
                Fee= 18
             },new TollFee{
                From= "17:00",
                To= "17:59",
                Fee= 14
             },new TollFee{
                From= "18:00",
                To= "18:29",
                Fee= 9
             },new TollFee{
                From= "18:30",
                To= "EOD",
                Fee= 0
             }
             }
            };
        }
        /// <summary>
        /// TODO the logic may need to be revisted
        /// </summary>
        /// <param name="tollDates"></param>
        /// <returns></returns>
        public int CalulateTotalFeeCost(List<DateTime> tollDates)
        {

            try
            {
                int totalFee = 0;
                DateTime intervalStart = tollDates[0];          
                int previousFee = 0;
                int accumelatedfeewithinhour=0;

                foreach (DateTime date in tollDates)
                {
                    TimeSpan timedifference = date.Subtract(intervalStart);
                    double minutes = timedifference.TotalMinutes;
                    int currentFee= GetTollFeePass(date);
                    if (minutes > 60)
                    {
                        totalFee += currentFee;
                        intervalStart = date;
                    }
                    else
                    {
                       
                        if (currentFee > previousFee)
                            accumelatedfeewithinhour= currentFee;                        

                    }

                    previousFee= currentFee;
                }
                totalFee += accumelatedfeewithinhour;
                if (totalFee > TaxRules.MaxTollfeePerDay) totalFee = 60;
                return totalFee;
            }
            catch (Exception )
            {
               
                throw;
            }
        }

        private int GetTollFeePass(DateTime date)
        {
          
            if (IsTollFreeDate(date)) return 0;
            
            TimeSpan time =date.TimeOfDay;

            var fee = TaxRules.TaxFees.FirstOrDefault(item => time >= TimeSpan.Parse(item.From) && time <= TimeSpan.Parse(item.To)||
            time >= TimeSpan.Parse("18:30") && string.Equals(item.To, "EOD"))?.Fee;

            return fee ?? 0; 
        }

        //Gets free dates
        private bool IsTollFreeDate(DateTime date)
        {
            bool result = (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);

            return result;

        }



    }
}

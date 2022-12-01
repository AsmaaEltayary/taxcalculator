using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollFeeCalculator;

namespace TollFreeCalculator.Test
{
    [TestClass]
    public class TollFreeCalculatorTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]

        [DataRow(new string[] { "2020-06-30 15:00", "2020-06-30 15:15", "2020-06-30 15:31" }, 18, DisplayName = "GetTaxWithin60Minute")]

        [DataRow(new string[] { "2020-06-30 00:05", "2020-06-30 06:34", "2020-06-30 08:52" }, 23, DisplayName = "GetTaxWithMore60Minute")]

        [DataRow(new string[] {"2020-06-30 06:00", "2020-06-30 07:34", "2020-06-30 08:52", "2020-06-30 10:13", "2020-06-30 10:25", "2020-06-30 11:04","2020-06-30 16:50", "2020-06-30 18:15","2020-06-30 18:15" }, 60, DisplayName = "GetTaxWithinabove60")]

        [DataRow(new string[] {"2020-06-30 00:05", "2020-06-30 06:34", "2020-06-30 08:52", "2020-06-30 10:13", "2020-06-30 10:25", "2020-06-30 11:04","2020-06-30 16:50","2020-06-30 18:00","2020-06-30 21:30","2020-07-01 00:00" }, 59, DisplayName = "TestData")]

        [DataRow(new string[] { "2020-06-14 21:00" }, 0, DisplayName = "GetTaxInWeekEnd")]

        public void GetTaxTests(string[] stringDates, int result)
        {
            List<DateTime> tollDates = new List<DateTime>();

            for (int i = 0; i < stringDates.Length; i++)
            {
                
                tollDates.Add(DateTime.ParseExact(stringDates[i], "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture));
            }

            int expectedValue = result;



            int ActualValue = new TaxCalculator().CalulateTotalFeeCost(tollDates);
         

            TestContext.WriteLine($"actual value is {ActualValue}");
            Assert.AreEqual(expectedValue, ActualValue);
        }


    }
}

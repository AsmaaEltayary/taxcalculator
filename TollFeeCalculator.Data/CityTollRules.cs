using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public class CityTollRules
    {

        public int MaxTollfeePerDay { get; set; }

        public List<TollFee> TaxFees { get; set; } = new List<TollFee>();
    }
}

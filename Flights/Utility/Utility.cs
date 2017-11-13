using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Flights.Utility
{
    public class Utility
    {
        public static IDictionary<string, string> GetCurrencySymbols()
        {
            var result = new Dictionary<string, string>();

            foreach (var ci in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                try
                {
                    var ri = new RegionInfo(ci.Name);
                    result[ri.ISOCurrencySymbol] = ri.CurrencySymbol;
                }
                catch { }
            }

            return result;
        }
    }
}
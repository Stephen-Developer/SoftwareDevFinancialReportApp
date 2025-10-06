using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Systems
{
    internal class TaxSystem
    {
        private static TaxSystem _instance;

        public static TaxSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TaxSystem();
                }
                return _instance;
            }
        }

        public bool useCustomTax = false;

        public readonly List<TaxBracket> defaultTaxBracketList = new List<TaxBracket>
        {
            new TaxBracket (0, 20, 20),
            new TaxBracket (20, null, 40)
        };

        public List<TaxBracket> customTaxBracketList = new List<TaxBracket>();

        public List<decimal> taxCredits = new List<decimal>();

        public TaxSystem()
        {

        }

        public void AddTaxBracket(decimal lowerBoundary, decimal? upperBoundary, decimal rate)
        {
            var taxBracket = new TaxBracket(lowerBoundary, upperBoundary, rate);
            customTaxBracketList.Add(taxBracket);
        }
    }
}

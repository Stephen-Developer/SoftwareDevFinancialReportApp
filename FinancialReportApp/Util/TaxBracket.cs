using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Util
{
    public class TaxBracket
    {
        private readonly decimal lowerBoundary;
        private readonly decimal? upperBoundary;
        private readonly decimal rate;

        public decimal LowerBoundary { get => lowerBoundary; }
        public decimal? UpperBoundary { get => upperBoundary; }
        public decimal Rate { get => rate; }

        public TaxBracket(decimal lowerBoundary, decimal? upperBoundary, decimal rate)
        {
            this.lowerBoundary = lowerBoundary;
            this.upperBoundary = upperBoundary;
            this.rate = rate;
        }
    }
}

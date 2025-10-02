using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Util
{
    internal class TaxBracket
    {
        public decimal LowerBoundry { get; set; }
        public decimal? UpperBoundry { get; set; }
        public decimal Rate { get; set; }
    }
}

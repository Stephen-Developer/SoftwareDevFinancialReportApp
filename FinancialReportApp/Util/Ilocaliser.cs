using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Util
{
    public interface ILocaliser
    {
        string Get(string key);
        string Get(string key, params object[] args);
    }
}

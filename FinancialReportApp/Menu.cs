using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp
{
    abstract class Menu
    {
        protected Dictionary<int, (string label, Action action)> menuActions = new Dictionary<int, (string label, Action action)>();



        public void Display()
        {

        }
    }
}

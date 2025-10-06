using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    public interface IUserInterface
    {
        void Clear();
        void WriteLine(string text);
        void Write(string text);
        string? ReadLine();
        void WaitForKey();
    }
}

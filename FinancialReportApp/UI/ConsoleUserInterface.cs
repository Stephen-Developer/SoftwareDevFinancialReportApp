using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    internal class ConsoleUserInterface : IUserInterface
    {
        public void Clear() => Console.Clear();

        public string? ReadLine() => Console.ReadLine();

        public void WaitForKey() => Console.ReadKey(false);

        public void Write(string text) => Console.Write(text);

        public void WriteLine(string text) => Console.WriteLine(text);
    }
}

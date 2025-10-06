using System;

namespace FinancialReportApp.Util
{
    public interface IInputHandler
    {
        string ReadLine();
        void Write(string message);
        void WriteLine(string message);
    }

    public class ConsoleInputHandler : IInputHandler
    {
        public string ReadLine() => Console.ReadLine();
        public void Write(string message) => Console.Write(message);
        public void WriteLine(string message) => Console.WriteLine(message);
    }
}

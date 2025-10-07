using FinancialReportApp.Systems;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    internal class ReportUI : UIBase
    {
        const string outputFileName = "FinancialReport.txt";

        private static readonly Lazy<ReportUI> _instance = new(() => new ReportUI(new ConsoleUserInterface()));

        public static ReportUI Instance => _instance.Value;

        public ReportUI(IUserInterface userInterface) : base(userInterface)
        {

        }

        public override void Display()
        {
            ReportGenerator.Instance.ProcessData();
            var report = ReportGenerator.Instance.GenerateReport();
            Console.WriteLine(report);
            Console.ReadLine();

            Console.Clear();

            var writeReport = InputHandler.PromtYesNo("Would you like to write the report to a file?");
            if (writeReport)
            {
                var exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var outputPath = Path.Combine(exeDirectory, outputFileName);

                var writeSuccess= FileUtils.WriteToFile(report, outputPath);
                if(writeSuccess)
                {
                    Console.WriteLine($"Report written to {outputPath}");
                }
                else
                {
                    Console.WriteLine($"Failed to write report to {outputPath}");
                }
                Console.ReadLine();
            }
        }
    }
}

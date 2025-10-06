using FinancialReportApp.Systems;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    internal class ReportUI : UI
    {
        private static ReportUI _instance;

        const string outputFileName = "FinancialReport.txt";

        public static ReportUI Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ReportUI();
                }
                return _instance;
            }
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

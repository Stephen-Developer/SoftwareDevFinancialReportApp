using FinancialReportApp.Systems;
using FinancialReportApp.UI.Menus;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI.UIs
{
    [Menu("Generate Report", typeof(MainMenu))]
    internal class ReportUI : UIBase
    {
        const string outputFileName = "FinancialReport.txt";

        private readonly IInputHandler inputHandler;
        private readonly IReportGenerator reportGenerator;

        public ReportUI(IUserInterface userInterface, IInputHandler inputHandler, IReportGenerator reportGenerator) : base(userInterface)
        {
            this.inputHandler = inputHandler;
            this.reportGenerator = reportGenerator;
        }

        public override void Display()
        {
            reportGenerator.ProcessData();
            var report = reportGenerator.GenerateReport();
            userInterface.WriteLine(report);
            userInterface.WaitForKey();

            userInterface.Clear();

            var writeReport = inputHandler.PromptYesNo("Would you like to write the report to a file?");
            //TODO: Look into abstracting file writing to a service
            if (writeReport)
            {
                var exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var outputPath = Path.Combine(exeDirectory, outputFileName);

                var writeSuccess= FileUtils.WriteToFile(report, outputPath);
                if(writeSuccess)
                {
                    userInterface.WriteLine($"Report written to {outputPath}");
                }
                else
                {
                    userInterface.WriteLine($"Failed to write report to {outputPath}");
                }
                userInterface.ReadLine();
            }
        }
    }
}

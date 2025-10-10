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
        private readonly IFileService fileService;

        public ReportUI(IUserInterface userInterface, IInputHandler inputHandler, IReportGenerator reportGenerator, IFileService fileService) : base(userInterface)
        {
            this.inputHandler = inputHandler;
            this.reportGenerator = reportGenerator;
            this.fileService = fileService;
        }

        public override void Display()
        {
            reportGenerator.ProcessData();
            var report = reportGenerator.GenerateReport();
            userInterface.WriteLine(report);
            userInterface.WaitForKey();

            userInterface.Clear();

            var writeReport = inputHandler.PromptYesNo("Would you like to write the report to a file?");

            if (!writeReport)   
                return;

            var exeDirectory = fileService.GetAppDirectory();
            var outputPath = Path.Combine(exeDirectory, outputFileName);

            var writeSuccess = fileService.TryWriteFile(outputPath, report);
            if (writeSuccess)
            {
                userInterface.WriteLine($"Report written to {outputPath}");
            }
            else
            {
                userInterface.WriteLine($"Failed to write report to {outputPath}");
            }
            userInterface.WaitForKey();
        }
    }
}

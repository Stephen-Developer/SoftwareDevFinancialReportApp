using FinancialReportApp.Resources;
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
    [Menu(nameof(Strings.ReportUI_Menu), typeof(MainMenu))]
    internal class ReportUI : UIBase
    {
        const string outputFileName = "FinancialReport.txt";

        private readonly IInputHandler inputHandler;
        private readonly IReportGenerator reportGenerator;
        private readonly IFileService fileService;

        public ReportUI(IUserInterface userInterface, ILocaliser localiser, IInputHandler inputHandler, IReportGenerator reportGenerator, IFileService fileService) : base(userInterface, localiser)
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

            var writeReport = PromptWriteReport();

            if (!writeReport)   
                return;

            var exeDirectory = fileService.GetAppDirectory();
            var outputPath = Path.Combine(exeDirectory, outputFileName);

            TryWriteFile(outputPath, report);
        }

        private bool PromptWriteReport()
        {
            var message = localiser.Get(nameof(Strings.ReportUI_Prompt_Write));
            return inputHandler.PromptYesNo(message);
        }

        private void TryWriteFile(string outputPath, string report)
        {
            var writeSuccess = fileService.TryWriteFile(outputPath, report);
            if (writeSuccess)
            {
                var message = localiser.Get(nameof(Strings.ReportUI_Message_Success), outputPath);
                userInterface.WriteLine(message);
            }
            else
            {
                var message = localiser.Get(nameof(Strings.ReportUI_Message_Failure), outputPath);
                userInterface.WriteLine(message);
            }
            userInterface.WaitForKey();
        }
    }
}

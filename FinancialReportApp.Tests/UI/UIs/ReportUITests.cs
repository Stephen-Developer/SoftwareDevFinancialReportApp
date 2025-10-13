using FinancialReportApp.Resources;
using FinancialReportApp.Systems;
using FinancialReportApp.UI;
using FinancialReportApp.UI.UIs;
using FinancialReportApp.Util;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Tests.UI.UIs
{
    [TestClass]
    public class ReportUITests
    {
        const string promptText = "Prompt";
        const string successText = "Success";
        const string failureText = "Failure";
        const string reportContent = "Report content";

        ReportUI ui;

        Mock<IUserInterface> mockUserInterface;
        Mock<IInputHandler> mockInputHandler;
        Mock<IReportGenerator> mockReportGenerator;
        Mock<IFileService> mockFileService;
        Mock<ILocaliser> mockLocaliser;

        [TestInitialize]
        public void Setup()
        {
            mockUserInterface = new Mock<IUserInterface>();
            mockInputHandler = new Mock<IInputHandler>();
            mockReportGenerator = new Mock<IReportGenerator>();
            mockFileService = new Mock<IFileService>();
            mockLocaliser = new Mock<ILocaliser>();

            ui = new ReportUI(mockUserInterface.Object, mockLocaliser.Object, mockInputHandler.Object, mockReportGenerator.Object, mockFileService.Object);
        }

        [TestMethod]
        public void Display_NoWriteToFile()
        {
            var generatedReport = "Test report content";

            mockReportGenerator.Setup(rg => rg.ProcessData());
            mockReportGenerator.Setup(rg => rg.GenerateReport()).Returns(generatedReport);
            mockInputHandler.Setup(ih => ih.PromptYesNo(It.IsAny<string>())).Returns(false);

            mockLocaliser.Setup(l => l.Get(nameof(Strings.ReportUI_Prompt_Write))).Returns(promptText);

            ui.Display();

            mockReportGenerator.Verify(rg => rg.ProcessData(), Times.Once);
            mockReportGenerator.Verify(rg => rg.GenerateReport(), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine(generatedReport), Times.Once);
            mockUserInterface.Verify(ui => ui.WaitForKey(), Times.Once);
            mockUserInterface.Verify(ui => ui.Clear(), Times.Once);
            mockInputHandler.Verify(ih => ih.PromptYesNo(promptText), Times.Once);
        }

        [TestMethod]
        public void Display_WhenWriteReportSelected_SavesFileAndNotifiesUser()
        {
            const string outputPath = "C:\\Temp\\FinancialReport.txt";

            mockReportGenerator.Setup(r => r.GenerateReport()).Returns(reportContent);
            mockInputHandler.Setup(i => i.PromptYesNo(It.IsAny<string>())).Returns(true);
            mockFileService.Setup(f => f.TryWriteFile(It.IsAny<string>(), reportContent)).Returns(true);
            mockFileService.Setup(f => f.GetAppDirectory()).Returns("C:\\Temp");

            mockLocaliser.Setup(l => l.Get(nameof(Strings.ReportUI_Prompt_Write))).Returns(promptText);
            mockLocaliser.Setup(l => l.Get(nameof(Strings.ReportUI_Message_Success), outputPath)).Returns(successText);

            ui.Display();

            mockFileService.Verify(f => f.TryWriteFile(outputPath, reportContent), Times.Once);
            mockUserInterface.Verify(u => u.WriteLine(successText), Times.Once);
        }
    }
}

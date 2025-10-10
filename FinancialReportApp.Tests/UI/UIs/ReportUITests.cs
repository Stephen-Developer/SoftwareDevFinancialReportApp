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
        ReportUI ui;

        Mock<IUserInterface> mockUserInterface;
        Mock<IInputHandler> mockInputHandler;
        Mock<IReportGenerator> mockReportGenerator;
        Mock<IFileService> mockFileService;

        [TestInitialize]
        public void Setup()
        {
            mockUserInterface = new Mock<IUserInterface>();
            mockInputHandler = new Mock<IInputHandler>();
            mockReportGenerator = new Mock<IReportGenerator>();
            mockFileService = new Mock<IFileService>();

            ui = new ReportUI(mockUserInterface.Object, mockInputHandler.Object, mockReportGenerator.Object, mockFileService.Object);
        }

        [TestMethod]
        public void Display_NoWriteToFile()
        {
            var generatedReport = "Test report content";

            mockReportGenerator.Setup(rg => rg.ProcessData());
            mockReportGenerator.Setup(rg => rg.GenerateReport()).Returns(generatedReport);
            mockInputHandler.Setup(ih => ih.PromptYesNo(It.IsAny<string>())).Returns(false);

            ui.Display();

            mockReportGenerator.Verify(rg => rg.ProcessData(), Times.Once);
            mockReportGenerator.Verify(rg => rg.GenerateReport(), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine(generatedReport), Times.Once);
            mockUserInterface.Verify(ui => ui.WaitForKey(), Times.Once);
            mockUserInterface.Verify(ui => ui.Clear(), Times.Once);
            mockInputHandler.Verify(ih => ih.PromptYesNo("Would you like to write the report to a file?"), Times.Once);
        }

        [TestMethod]
        public void Display_WhenWriteReportSelected_SavesFileAndNotifiesUser()
        {
            mockReportGenerator.Setup(r => r.GenerateReport()).Returns("REPORT CONTENT");
            mockInputHandler.Setup(i => i.PromptYesNo(It.IsAny<string>())).Returns(true);
            mockFileService.Setup(f => f.TryWriteFile(It.IsAny<string>(), "REPORT CONTENT")).Returns(true);
            mockFileService.Setup(f => f.GetAppDirectory()).Returns("C:\\Temp");

            ui.Display();

            mockFileService.Verify(f => f.TryWriteFile("C:\\Temp\\FinancialReport.txt", "REPORT CONTENT"), Times.Once);
            mockUserInterface.Verify(u => u.WriteLine(It.Is<string>(s => s.Contains("Report written"))), Times.Once);
        }
    }
}

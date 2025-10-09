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

        [TestInitialize]
        public void Setup()
        {
            mockUserInterface = new Mock<IUserInterface>();
            mockInputHandler = new Mock<IInputHandler>();
            mockReportGenerator = new Mock<IReportGenerator>();

            ui = new ReportUI(mockUserInterface.Object, mockInputHandler.Object, mockReportGenerator.Object);
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
    }
}

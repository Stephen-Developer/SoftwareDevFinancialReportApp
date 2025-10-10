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
    public class RemoveTaxCreditUITests
    {
        RemoveTaxCreditUI ui;

        Mock<IUserInterface> mockUserInterface;
        Mock<IInputHandler> mockInputHandler;
        Mock<IUserData> mockUserData;

        [TestInitialize]
        public void Setup()
        {
            mockUserInterface = new Mock<IUserInterface>();
            mockInputHandler = new Mock<IInputHandler>();
            mockUserData = new Mock<IUserData>();

            ui = new RemoveTaxCreditUI(mockUserInterface.Object, mockInputHandler.Object, mockUserData.Object);
        }

        [TestMethod]
        public void Display_NoCredits_ShowsNoCreditsMessage()
        {
            mockUserData.Setup(ud => ud.TaxCredits).Returns(new List<decimal>());

            ui.Display();

            mockUserInterface.Verify(ui => ui.WriteLine("No credits to remove."), Times.Once);
            mockUserInterface.Verify(ui => ui.WaitForKey(), Times.Once);
        }


        [TestMethod]
        public void Display_WithCredits_RemovesSelectedCredit()
        {
            var credits = new List<decimal> { 1000m, 2000m, 3000m };

            mockUserData.Setup(ud => ud.TaxCredits).Returns(credits);
            mockInputHandler.Setup(ih => ih.PromptInt(It.IsAny<string>())).Returns(2);

            ui.Display();

            mockUserInterface.Verify(ui => ui.WriteLine("Current credits:"), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine("1. 1000"), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine("2. 2000"), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine("3. 3000"), Times.Once);
            mockUserData.Verify(ud => ud.RemoveTaxCredit(1), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine("Expense removed."), Times.Once);
            mockUserInterface.Verify(ui => ui.WaitForKey(), Times.Once);
        }
    }
}

using FinancialReportApp.Resources;
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
        const string nothingText = "Nothing";
        const string currentText = "Current";
        const string removedText = "Removed";
        const string invalidText = "Invalid";
        const string promptText = "Prompt";

        RemoveTaxCreditUI ui;

        Mock<IUserInterface> mockUserInterface;
        Mock<IInputHandler> mockInputHandler;
        Mock<IUserData> mockUserData;
        Mock<ILocaliser> mockLocaliser;

        [TestInitialize]
        public void Setup()
        {
            mockUserInterface = new Mock<IUserInterface>();
            mockInputHandler = new Mock<IInputHandler>();
            mockUserData = new Mock<IUserData>();
            mockLocaliser = new Mock<ILocaliser>();

            ui = new RemoveTaxCreditUI(mockUserInterface.Object, mockLocaliser.Object, mockInputHandler.Object, mockUserData.Object);
        }

        [TestMethod]
        public void Display_NoCredits_ShowsNoCreditsMessage()
        {
            mockUserData.Setup(ud => ud.TaxCredits).Returns(new List<decimal>());

            mockLocaliser.Setup(l => l.Get(nameof(Strings.RemoveTaxCreditUI_Message_Nothing))).Returns(nothingText);

            ui.Display();

            mockUserInterface.Verify(ui => ui.WriteLine(nothingText), Times.Once);
            mockUserInterface.Verify(ui => ui.WaitForKey(), Times.Once);
        }


        [TestMethod]
        public void Display_WithCredits_RemovesSelectedCredit()
        {
            var credits = new List<decimal> { 1000m, 2000m, 3000m };

            mockUserData.Setup(ud => ud.TaxCredits).Returns(credits);
            mockInputHandler.Setup(ih => ih.PromptInt(It.IsAny<string>())).Returns(2);

            mockLocaliser.Setup(l => l.Get(nameof(Strings.RemoveTaxCreditUI_Message_Current))).Returns(currentText);
            mockLocaliser.Setup(l => l.Get(nameof(Strings.RemoveTaxCreditUI_Prompt_Number))).Returns(promptText);
            mockLocaliser.Setup(l => l.Get(nameof(Strings.RemoveTaxCreditUI_Message_Removed))).Returns(removedText);

            ui.Display();

            mockUserInterface.Verify(ui => ui.WriteLine(currentText), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine("1. 1000"), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine("2. 2000"), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine("3. 3000"), Times.Once);
            mockUserData.Verify(ud => ud.RemoveTaxCredit(1), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine(removedText), Times.Once);
            mockUserInterface.Verify(ui => ui.WaitForKey(), Times.Once);
        }

        [TestMethod]
        public void Display_WithCredits_RemovesInvalid()
        {
            var credits = new List<decimal> { 1000m, 2000m, 3000m };

            mockUserData.Setup(ud => ud.TaxCredits).Returns(credits);
            mockInputHandler.Setup(ih => ih.PromptInt(It.IsAny<string>())).Returns(5);

            mockLocaliser.Setup(l => l.Get(nameof(Strings.RemoveTaxCreditUI_Message_Current))).Returns(currentText);
            mockLocaliser.Setup(l => l.Get(nameof(Strings.RemoveTaxCreditUI_Prompt_Number))).Returns(promptText);
            mockLocaliser.Setup(l => l.Get(nameof(Strings.RemoveTaxCreditUI_Message_Invalid))).Returns(invalidText);

            ui.Display();

            mockUserInterface.Verify(ui => ui.WriteLine(currentText), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine("1. 1000"), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine("2. 2000"), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine("3. 3000"), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine(invalidText), Times.Once);
            mockUserInterface.Verify(ui => ui.WaitForKey(), Times.Once);
        }
    }
}

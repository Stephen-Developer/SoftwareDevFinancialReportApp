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
    public class ClearTaxCreditsUITests
    {
        const string promptValue = "Prompt";
        const string clearValue = "Clear";
        const string cancelValue = "Cancel";

        ClearTaxCreditsUI ui;

        Mock<IUserInterface> userInterface;
        Mock<IInputHandler> inputHandler;
        Mock<IUserData> userData;
        Mock<ILocaliser> localiser;

        [TestInitialize]
        public void Setup()
        {
            userInterface = new Mock<IUserInterface>();
            inputHandler = new Mock<IInputHandler>();
            userData = new Mock<IUserData>();
            localiser = new Mock<ILocaliser>();

            ui = new ClearTaxCreditsUI(userInterface.Object, inputHandler.Object, userData.Object, localiser.Object);
        }

        [TestMethod]
        public void ClearTaxCreditsUTest()
        {
            inputHandler.Setup(i => i.PromptYesNo(It.IsAny<string>())).Returns(true);
            localiser.Setup(l => l.Get(nameof(Strings.ClearTaxCreditsUI_Prompt))).Returns(promptValue);
            localiser.Setup(l => l.Get(nameof(Strings.ClearTaxCreditsUI_Message_Cleared))).Returns(clearValue);

            ui.Display();

            localiser.Verify(l => l.Get(nameof(Strings.ClearTaxCreditsUI_Prompt)), Times.Once);
            inputHandler.Verify(i => i.PromptYesNo(promptValue), Times.Once);
            userData.Verify(u => u.ClearTaxCredits(), Times.Once);
            localiser.Verify(l => l.Get(nameof(Strings.ClearTaxCreditsUI_Message_Cleared)), Times.Once);
            userInterface.Verify(u => u.WriteLine(clearValue), Times.Once);
            userInterface.Verify(u => u.WaitForKey(), Times.Once);
        }

        [TestMethod]
        public void DeclineClearTaxCreditsUTest()
        {
            inputHandler.Setup(i => i.PromptYesNo(It.IsAny<string>())).Returns(false);
            localiser.Setup(l => l.Get(nameof(Strings.ClearTaxCreditsUI_Prompt))).Returns(promptValue);
            localiser.Setup(l => l.Get(nameof(Strings.ClearTaxCreditsUI_Message_Cancelled))).Returns(cancelValue);

            ui.Display();

            localiser.Verify(l => l.Get(nameof(Strings.ClearTaxCreditsUI_Prompt)), Times.Once);
            inputHandler.Verify(i => i.PromptYesNo(promptValue), Times.Once);
            userData.Verify(u => u.ClearTaxCredits(), Times.Never);
            localiser.Verify(l => l.Get(nameof(Strings.ClearTaxCreditsUI_Message_Cancelled)), Times.Once);
            userInterface.Verify(u => u.WriteLine(cancelValue), Times.Once);
            userInterface.Verify(u => u.WaitForKey(), Times.Once);
        }
    }
}

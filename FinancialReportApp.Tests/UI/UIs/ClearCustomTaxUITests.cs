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
    public class ClearCustomTaxUITests
    {
        const string promptText = "Prompt";
        const string clearText = "Clear";
        const string cancelText = "Cancel";

        ClearCustomTaxUI ui;

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

            ui = new ClearCustomTaxUI(userInterface.Object, inputHandler.Object, userData.Object, localiser.Object);
        }

        [TestMethod]
        public void NoCustomTaxBrackets_DoesNothing()
        {
            userData.SetupGet(u => u.CustomTaxBrackets).Returns(new List<TaxBracket>());

            ui.Display();

            userInterface.Verify(u => u.WriteLine(It.IsAny<string>()), Times.Never);
            userInterface.Verify(u => u.WaitForKey(), Times.Never);
            inputHandler.Verify(i => i.PromptYesNo(It.IsAny<string>()), Times.Never);
            userData.Verify(u => u.ClearTaxBrackets(), Times.Never);
        }

        [TestMethod]
        public void UserConfirms_ClearsTaxBrackets()
        {
            userData.SetupGet(u => u.CustomTaxBrackets).Returns(new List<TaxBracket> { new TaxBracket(0, 10000, 10) });
            inputHandler.Setup(i => i.PromptYesNo(It.IsAny<string>())).Returns(true);
            localiser.Setup(u => u.Get(nameof(Strings.ClearCustomTaxUI_Prompt))).Returns(promptText);
            localiser.Setup(u => u.Get(nameof(Strings.ClearCustomTaxUI_Message_Cleared))).Returns(clearText);
            
            ui.Display();

            localiser.Verify(l => l.Get(nameof(Strings.ClearCustomTaxUI_Prompt)), Times.Once);
            localiser.Verify(l => l.Get(nameof(Strings.ClearCustomTaxUI_Message_Cleared)), Times.Once);
            inputHandler.Verify(i => i.PromptYesNo(promptText), Times.Once);
            userData.Verify(u => u.ClearTaxBrackets(), Times.Once);
            userInterface.Verify(u => u.WriteLine(clearText), Times.Once);
            userInterface.Verify(u => u.WaitForKey(), Times.Once);
        }

        [TestMethod]
        public void UserDeclines_DoesNotClearTaxBrackets()
        {
            userData.SetupGet(u => u.CustomTaxBrackets).Returns(new List<TaxBracket> { new TaxBracket(0, 10000, 10) });
            inputHandler.Setup(i => i.PromptYesNo(It.IsAny<string>())).Returns(false);
            localiser.Setup(l => l.Get(nameof(Strings.ClearCustomTaxUI_Prompt))).Returns(promptText);
            localiser.Setup(l => l.Get(nameof(Strings.ClearCustomTaxUI_Message_Cancelled))).Returns(cancelText);
            
            ui.Display();

            localiser.Verify(l => l.Get(nameof(Strings.ClearCustomTaxUI_Prompt)), Times.Once);
            localiser.Verify(l => l.Get(nameof(Strings.ClearCustomTaxUI_Message_Cancelled)), Times.Once);
            inputHandler.Verify(i => i.PromptYesNo(promptText), Times.Once);
            userData.Verify(u => u.ClearTaxBrackets(), Times.Never);
            userInterface.Verify(u => u.WriteLine(cancelText), Times.Once);
            userInterface.Verify(u => u.WaitForKey(), Times.Once);
        }
    }
}

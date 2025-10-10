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
        ClearCustomTaxUI ui;

        Mock<IUserInterface> userInterface;
        Mock<IInputHandler> inputHandler;
        Mock<IUserData> userData;

        [TestInitialize]
        public void Setup()
        {
            userInterface = new Mock<IUserInterface>();
            inputHandler = new Mock<IInputHandler>();
            userData = new Mock<IUserData>();

            ui = new ClearCustomTaxUI(userInterface.Object, inputHandler.Object, userData.Object);
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
            
            ui.Display();

            inputHandler.Verify(i => i.PromptYesNo("Are you sure you want to clear all custom tax brackets?"), Times.Once);
            userData.Verify(u => u.ClearTaxBrackets(), Times.Once);
            userInterface.Verify(u => u.WriteLine("Custom tax brackets cleared."), Times.Once);
            userInterface.Verify(u => u.WaitForKey(), Times.Once);
        }

        [TestMethod]
        public void UserDeclines_DoesNotClearTaxBrackets()
        {
            userData.SetupGet(u => u.CustomTaxBrackets).Returns(new List<TaxBracket> { new TaxBracket(0, 10000, 10) });
            inputHandler.Setup(i => i.PromptYesNo(It.IsAny<string>())).Returns(false);
            
            ui.Display();

            inputHandler.Verify(i => i.PromptYesNo("Are you sure you want to clear all custom tax brackets?"), Times.Once);
            userData.Verify(u => u.ClearTaxBrackets(), Times.Never);
            userInterface.Verify(u => u.WriteLine("Operation cancelled."), Times.Once);
            userInterface.Verify(u => u.WaitForKey(), Times.Once);
        }
    }
}

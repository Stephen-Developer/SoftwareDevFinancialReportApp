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
        ClearTaxCreditsUI ui;

        Mock<IUserInterface> userInterface;
        Mock<IInputHandler> inputHandler;
        Mock<IUserData> userData;

        [TestInitialize]
        public void Setup()
        {
            userInterface = new Mock<IUserInterface>();
            inputHandler = new Mock<IInputHandler>();
            userData = new Mock<IUserData>();

            ui = new ClearTaxCreditsUI(userInterface.Object, inputHandler.Object, userData.Object);
        }

        [TestMethod]
        public void ClearTaxCreditsUTest()
        {
            inputHandler.Setup(i => i.PromptYesNo(It.IsAny<string>())).Returns(true);

            ui.Display();

            inputHandler.Verify(i => i.PromptYesNo("Are you sure you want to clear all tax credits?"), Times.Once);
            userData.Verify(u => u.ClearTaxCredits(), Times.Once);
            userInterface.Verify(u => u.WriteLine("All tax credits cleared."), Times.Once);
            userInterface.Verify(u => u.WaitForKey(), Times.Once);
        }

        [TestMethod]
        public void DeclineClearTaxCreditsUTest()
        {
            inputHandler.Setup(i => i.PromptYesNo(It.IsAny<string>())).Returns(false);

            ui.Display();

            inputHandler.Verify(i => i.PromptYesNo("Are you sure you want to clear all tax credits?"), Times.Once);
            userData.Verify(u => u.ClearTaxCredits(), Times.Never);
            userInterface.Verify(u => u.WriteLine("Tax credits not cleared."), Times.Once);
            userInterface.Verify(u => u.WaitForKey(), Times.Once);
        }
    }
}

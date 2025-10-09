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
    public class InputCustomTaxUITests
    {
        InputCustomTaxUI ui;

        Mock<IUserInterface> userInterface;
        Mock<IInputHandler> inputHandler;
        Mock<IUserData> userData;
        Mock<IUIFlowController> flowController;

        [TestInitialize]
        public void Setup()
        {
            userInterface = new Mock<IUserInterface>();
            inputHandler = new Mock<IInputHandler>();
            userData = new Mock<IUserData>();
            flowController = new Mock<IUIFlowController>();

            ui = new InputCustomTaxUI(userInterface.Object, inputHandler.Object, userData.Object, flowController.Object);
        }

        [TestMethod]
        public void InputCustomTaxUTest()
        {
            List<TaxBracket> brackets = new List<TaxBracket>();
            userData.Setup(u => u.CustomTaxBrackets).Returns(brackets);
            userData.Setup(u => u.AddTaxBracket(It.IsAny<decimal>(), It.IsAny<decimal?>(), It.IsAny<decimal>()))
                .Callback<decimal, decimal?, decimal>((lower, upper, rate) =>
                {
                    brackets.Add(new TaxBracket(lower, upper, rate));
                });
            inputHandler.SetupSequence(i => i.PromptNullableDecimal(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns(30000)  // First upper limit
                .Returns(60000)  // Second upper limit
                .Returns((decimal?)null); // No upper limit for the last bracket
            inputHandler.SetupSequence(i => i.PromptDecimal(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns(20)  // First rate
                .Returns(40)  // Second rate
                .Returns(45); // Last rate
            flowController.Setup(f => f.NavigateTo(typeof(ClearCustomTaxUI)));

            ui.Display();

            flowController.Verify(f => f.NavigateTo(typeof(ClearCustomTaxUI)), Times.Once);
            userInterface.Verify(u => u.Clear(), Times.Exactly(3));
            userInterface.Verify(u => u.WriteLine("Custom Tax Bracket starting point: 0"), Times.Once);
            userInterface.Verify(u => u.WriteLine("Custom Tax Bracket starting point: 30000"), Times.Once);
            userInterface.Verify(u => u.WriteLine("Custom Tax Bracket starting point: 60000"), Times.Once);
            inputHandler.Verify(i => i.PromptNullableDecimal("Enter upper boundary (or leave blank for no upper limit): ", 0, 30000), Times.Once);
            inputHandler.Verify(i => i.PromptNullableDecimal("Enter upper boundary (or leave blank for no upper limit): ", 30000, 60000), Times.Once);
            inputHandler.Verify(i => i.PromptNullableDecimal("Enter upper boundary (or leave blank for no upper limit): ", 60000, decimal.MaxValue), Times.Once);
            inputHandler.Verify(i => i.PromptDecimal("Enter tax rate (as a percentage): ", 0, 100), Times.Exactly(3));
            userData.Verify(u => u.AddTaxBracket(0, 30000, 0.20m), Times.Once);
            userData.Verify(u => u.AddTaxBracket(30000, 60000, 0.40m), Times.Once);
            userData.Verify(u => u.AddTaxBracket(60000, null, 0.45m), Times.Once);
            userInterface.Verify(u => u.WriteLine("The last tax bracket has no upper limit. Unable to add more brackets."), Times.Once);
        }
    }
}

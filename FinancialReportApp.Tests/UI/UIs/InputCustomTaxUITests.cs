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
    public class InputCustomTaxUITests
    {
        const string lowerText = "Lower";
        const string upperText = "Upper";
        const string rateText = "Rate";
        const string endText = "End";

        InputCustomTaxUI ui;

        Mock<IUserInterface> userInterface;
        Mock<IInputHandler> inputHandler;
        Mock<IUserData> userData;
        Mock<IUIFlowController> flowController;
        Mock<ILocaliser> localiser;

        [TestInitialize]
        public void Setup()
        {
            userInterface = new Mock<IUserInterface>();
            inputHandler = new Mock<IInputHandler>();
            userData = new Mock<IUserData>();
            flowController = new Mock<IUIFlowController>();
            localiser = new Mock<ILocaliser>();

            ui = new InputCustomTaxUI(userInterface.Object, localiser.Object, inputHandler.Object, userData.Object, flowController.Object);
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

            localiser.Setup(l => l.Get(nameof(Strings.InputCustomTaxUI_Message_Lower), 0m)).Returns(lowerText);
            localiser.Setup(l => l.Get(nameof(Strings.InputCustomTaxUI_Message_Lower), 30000m)).Returns(lowerText);
            localiser.Setup(l => l.Get(nameof(Strings.InputCustomTaxUI_Message_Lower), 60000m)).Returns(lowerText);
            localiser.Setup(l => l.Get(nameof(Strings.InputCustomTaxUI_Message_Upper))).Returns(upperText);
            localiser.Setup(l => l.Get(nameof(Strings.InputCustomTaxUI_Message_Rate))).Returns(rateText);
            localiser.Setup(l => l.Get(nameof(Strings.InputCustomTaxUI_Message_End))).Returns(endText);

            ui.Display();

            flowController.Verify(f => f.NavigateTo(typeof(ClearCustomTaxUI)), Times.Once);
            userInterface.Verify(u => u.Clear(), Times.Exactly(3));
            userInterface.Verify(u => u.WriteLine(lowerText), Times.Exactly(3));
            inputHandler.Verify(i => i.PromptNullableDecimal(upperText, 0m, decimal.MaxValue), Times.Once);
            inputHandler.Verify(i => i.PromptNullableDecimal(upperText, 30000m, decimal.MaxValue), Times.Once);
            inputHandler.Verify(i => i.PromptNullableDecimal(upperText, 60000m, decimal.MaxValue), Times.Once);
            inputHandler.Verify(i => i.PromptDecimal(rateText, 0, 100), Times.Exactly(3));
            userData.Verify(u => u.AddTaxBracket(0, 30000, 0.20m), Times.Once);
            userData.Verify(u => u.AddTaxBracket(30000, 60000, 0.40m), Times.Once);
            userData.Verify(u => u.AddTaxBracket(60000, null, 0.45m), Times.Once);
            userInterface.Verify(u => u.WriteLine(endText), Times.Once);
        }
    }
}

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
    public class InputTaxCreditUITests
    {
        const string amountText = "Amount";

        InputTaxCreditUI ui;

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

            ui = new InputTaxCreditUI(mockUserInterface.Object, mockLocaliser.Object, mockInputHandler.Object, mockUserData.Object);
        }

        [TestMethod]
        public void Display_ShouldPromptForTaxCreditAndAddToUserData()
        {
            decimal expectedCredit = 2000m;
            mockInputHandler.Setup(ih => ih.PromptDecimal(It.IsAny<string>(), It.IsAny<decimal>(), decimal.MaxValue)).Returns(expectedCredit);

            mockLocaliser.Setup(l => l.Get(nameof(Strings.InputTaxCreditUI_Prompt_Amount))).Returns(amountText);
            
            ui.Display();
            
            mockUserInterface.Verify(ui => ui.Clear(), Times.Once);
            mockInputHandler.Verify(ih => ih.PromptDecimal(amountText, 0, decimal.MaxValue), Times.Once);
            mockUserData.Verify(ud => ud.AddTaxCredit(expectedCredit), Times.Once);
        }
    }
}

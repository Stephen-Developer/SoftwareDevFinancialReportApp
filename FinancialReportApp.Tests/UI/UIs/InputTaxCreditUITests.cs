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
        InputTaxCreditUI ui;

        Mock<IUserInterface> mockUserInterface;
        Mock<IInputHandler> mockInputHandler;
        Mock<IUserData> mockUserData;

        [TestInitialize]
        public void Setup()
        {
            mockUserInterface = new Mock<IUserInterface>();
            mockInputHandler = new Mock<IInputHandler>();
            mockUserData = new Mock<IUserData>();

            ui = new InputTaxCreditUI(mockUserInterface.Object, mockInputHandler.Object, mockUserData.Object);
        }

        [TestMethod]
        public void Display_ShouldPromptForTaxCreditAndAddToUserData()
        {
            // Arrange
            decimal expectedCredit = 2000m;
            mockInputHandler.Setup(ih => ih.PromptDecimal(It.IsAny<string>(), It.IsAny<decimal>(), decimal.MaxValue)).Returns(expectedCredit);
            // Act
            ui.Display();
            // Assert
            mockUserInterface.Verify(ui => ui.Clear(), Times.Once);
            mockInputHandler.Verify(ih => ih.PromptDecimal("Input tax credit/deductable: ", 0, decimal.MaxValue), Times.Once);
            mockUserData.Verify(ud => ud.AddTaxCredit(expectedCredit), Times.Once);
        }
    }
}

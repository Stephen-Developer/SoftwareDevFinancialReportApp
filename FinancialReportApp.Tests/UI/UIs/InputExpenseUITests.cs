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
    public  class InputExpenseUITests
    {
        InputExpenseItemUI ui;

        Mock<IUserInterface > mockUserInterface;
        Mock<IInputHandler> mockInputHandler;
        Mock<IUserData> mockUserData;

        [TestInitialize]
        public void Setup()
        {
            mockUserInterface = new Mock<IUserInterface>();
            mockInputHandler = new Mock<IInputHandler>();
            mockUserData = new Mock<IUserData>();

            ui = new InputExpenseItemUI(mockUserInterface.Object, mockInputHandler.Object, mockUserData.Object);
        }

        [TestMethod]
        public void Display_ShouldPromptForExpenseDetails_AndAddExpense()
        {
            
            string expectedCategory = "Food";
            decimal expectedAmount = 150.75m;
            TimeFrequency expectedFrequency = TimeFrequency.Monthly;
            mockInputHandler.Setup(m => m.PromptString(It.IsAny<string>())).Returns(expectedCategory);
            mockInputHandler.Setup(m => m.PromptDecimal(It.IsAny<string>(), decimal.MinValue, decimal.MaxValue)).Returns(expectedAmount);
            mockInputHandler.Setup(m => m.PromptEnum<TimeFrequency>(It.IsAny<string>())).Returns(expectedFrequency);
            
            ui.Display();
            
            mockInputHandler.Verify(m => m.PromptString("Enter expense category: "), Times.Once);
            mockInputHandler.Verify(m => m.PromptDecimal("Enter expense amount: ", decimal.MinValue, decimal.MaxValue), Times.Once);
            mockInputHandler.Verify(m => m.PromptEnum<TimeFrequency>("Enter expense frequency (e.g., Monthly, Weekly): "), Times.Once);
            mockUserData.Verify(m => m.AddExpense(expectedCategory, expectedAmount, expectedFrequency), Times.Once);
        }
    }
}

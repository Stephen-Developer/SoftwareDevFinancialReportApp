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
    public class RemoveExpenseItemUITests
    {
        RemoveExpenseItemUI ui;

        Mock<IUserInterface> mockUserInterface;
        Mock<IInputHandler> mockInputHandler;
        Mock<IUserData> mockUserData;

        [TestInitialize]
        public void Setup()
        {
            mockUserInterface = new Mock<IUserInterface>();
            mockInputHandler = new Mock<IInputHandler>();
            mockUserData = new Mock<IUserData>();

            ui = new RemoveExpenseItemUI(mockUserInterface.Object, mockInputHandler.Object, mockUserData.Object);
        }

        [TestMethod]
        public void Display_NoExpenses_ShowsNoExpensesMessage()
        {
            mockUserData.Setup(ud => ud.Expenses).Returns(new List<Expense>());
            
            ui.Display();
            
            mockUserInterface.Verify(ui => ui.WriteLine("No expenses to remove."), Times.Once);
            mockUserInterface.Verify(ui => ui.WaitForKey(), Times.Once);
        }

        [TestMethod]
        public void Display_WithExpenses_RemovesSelectedExpense()
        {
            var expenses = new List<Expense>
            {
                new Expense("Food", 100, TimeFrequency.Weekly),
                new Expense("Rent", 500, TimeFrequency.Monthly)
            };

            mockUserData.Setup(ud => ud.Expenses).Returns(expenses);
            mockInputHandler.Setup(ih => ih.PromptInt(It.IsAny<string>())).Returns(1); // Select first expense
            
            ui.Display();
            
            mockUserInterface.Verify(ui => ui.WriteLine("Current Expenses:"), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine("1. Food - 100 (Weekly)"), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine("2. Rent - 500 (Monthly)"), Times.Once);
            mockUserData.Verify(ud => ud.RemoveExpense(0), Times.Once); // Index 0 for first expense
            mockUserInterface.Verify(ui => ui.WriteLine("Expense removed."), Times.Once);
        }
    }
}

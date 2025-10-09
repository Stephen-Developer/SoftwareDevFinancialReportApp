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
    public class ViewExpensesUITests
    {
        ViewExpensesUI ui;

        Mock<IUserInterface> mockUserInterface;
        Mock<IUserData> mockUserData;

        [TestInitialize]
        public void Setup()
        {
            mockUserInterface = new Mock<IUserInterface>();
            mockUserData = new Mock<IUserData>();

            ui = new ViewExpensesUI(mockUserInterface.Object, mockUserData.Object);
        }

        [TestMethod]
        public void Display_NoExpenses()
        {
            mockUserData.Setup(ud => ud.Expenses).Returns(new List<Expense>());

            ui.Display();

            mockUserInterface.Verify(ui => ui.WriteLine("No expenses recorded."), Times.Once);
            mockUserInterface.Verify(ui => ui.WaitForKey(), Times.Once);
        }

        [TestMethod]
        public void Display_WithExpenses()
        {
            var expenses = new List<Expense>
            {
                new Expense("Food", 200, TimeFrequency.Weekly),
                new Expense("Rent", 1000, TimeFrequency.Monthly)
            };

            mockUserData.Setup(ud => ud.Expenses).Returns(expenses);

            ui.Display();

            mockUserInterface.Verify(ui => ui.WriteLine("Current Expenses:"), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine("Food - 200 (Weekly)"), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine("Rent - 1000 (Monthly)"), Times.Once);
            mockUserInterface.Verify(ui => ui.WaitForKey(), Times.Once);
        }
    }
}

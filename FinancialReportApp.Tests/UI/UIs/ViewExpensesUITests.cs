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
    public class ViewExpensesUITests
    {
        const string emptyText = "Empty";
        const string currentText = "Current";
        const string expenseText = "{0} - {1} ({2})";

        ViewExpensesUI ui;

        Mock<IUserInterface> mockUserInterface;
        Mock<IUserData> mockUserData;
        Mock<ILocaliser> mockLocaliser;

        [TestInitialize]
        public void Setup()
        {
            mockUserInterface = new Mock<IUserInterface>();
            mockUserData = new Mock<IUserData>();
            mockLocaliser = new Mock<ILocaliser>();

            ui = new ViewExpensesUI(mockUserInterface.Object, mockLocaliser.Object, mockUserData.Object);
        }

        [TestMethod]
        public void Display_NoExpenses()
        {
            mockUserData.Setup(ud => ud.Expenses).Returns(new List<Expense>());

            mockLocaliser.Setup(l => l.Get(nameof(Strings.ViewExpensesUI_Message_Empty))).Returns(emptyText);

            ui.Display();

            mockUserInterface.Verify(ui => ui.WriteLine(emptyText), Times.Once);
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

            mockLocaliser.Setup(l => l.Get(nameof(Strings.ViewExpensesUI_Message_Current))).Returns(currentText);
            mockLocaliser.Setup(l => l.Get(
                nameof(Strings.ViewExpensesUI_Message_Expense), It.IsAny<object[]>()))
                .Returns((string _, object[] args) => string.Format(expenseText, args));

            ui.Display();

            mockUserInterface.Verify(ui => ui.WriteLine(currentText), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine(string.Format(expenseText, expenses[0].Category, expenses[0].Amount, expenses[0].Frequency)), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine(string.Format(expenseText, expenses[1].Category, expenses[1].Amount, expenses[1].Frequency)), Times.Once);
            mockUserInterface.Verify(ui => ui.WaitForKey(), Times.Once);
        }
    }
}

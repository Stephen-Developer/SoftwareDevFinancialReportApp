using FinancialReportApp.Resources;
using FinancialReportApp.UI;
using FinancialReportApp.UI.Menus;
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
    public class SalaryAfterTaxUITests
    {
        const string salaryText = "Salary";
        const string taxText = "Tax";

        SalaryAfterTaxUI ui;

        Mock<IUserInterface> mockUserInterface;
        Mock<IUIFlowController> mockFlowController;
        Mock<IInputHandler> mockInputHandler;
        Mock<IUserData> mockUserData;
        Mock<ILocaliser> mockLocaliser;

        [TestInitialize]
        public void Setup()
        {
            mockUserInterface = new Mock<IUserInterface>();
            mockFlowController = new Mock<IUIFlowController>();
            mockInputHandler = new Mock<IInputHandler>();
            mockUserData = new Mock<IUserData>();
            mockLocaliser = new Mock<ILocaliser>();

            ui = new SalaryAfterTaxUI(mockUserInterface.Object, mockLocaliser.Object, mockFlowController.Object, mockInputHandler.Object, mockUserData.Object);
        }

        [TestMethod]
        public void Display_ShouldPromptForSalaryAndTaxAndNavigateToFrequencyMenu()
        {
            decimal expectedSalary = 50000m;
            decimal expectedTaxPaid = 10000m;

            mockInputHandler.SetupSequence(ih => ih.PromptDecimal(It.IsAny<string>(), decimal.MinValue, decimal.MaxValue))
                .Returns(expectedSalary)
                .Returns(expectedTaxPaid);

            mockLocaliser.Setup(l => l.Get(nameof(Strings.SalaryAfterTaxUI_Message_Salary))).Returns(salaryText);
            mockLocaliser.Setup(l => l.Get(nameof(Strings.SalaryAfterTaxUI_Message_Tax))).Returns(taxText);
            
            ui.Display();
            
            mockUserInterface.Verify(ui => ui.Clear(), Times.Once);
            mockInputHandler.Verify(ih => ih.PromptDecimal(salaryText, decimal.MinValue, decimal.MaxValue), Times.Once);
            mockUserData.VerifySet(ud => ud.Salary = expectedSalary, Times.Once);
            mockUserData.VerifySet(ud => ud.IsSalaryBeforeTax = false, Times.Once);
            mockFlowController.Verify(fc => fc.NavigateTo(typeof(SalaryFrequencyMenu)), Times.Once);
            mockInputHandler.Verify(ih => ih.PromptDecimal(taxText, decimal.MinValue, decimal.MaxValue), Times.Once);
            mockUserData.VerifySet(ud => ud.TaxPaid = expectedTaxPaid, Times.Once);
        }
    }
}

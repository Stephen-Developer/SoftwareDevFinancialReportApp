using FinancialReportApp.Resources;
using FinancialReportApp.UI;
using FinancialReportApp.UI.Menus;
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
    public class SalaryBeforeTaxUITests
    {
        const string salaryText = "Salary";

        SalaryBeforeTaxUI ui;

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

            ui = new SalaryBeforeTaxUI(mockUserInterface.Object, mockLocaliser.Object, mockFlowController.Object, mockInputHandler.Object, mockUserData.Object);
        }

        [TestMethod]
        public void Display_ShouldPromptForSalaryAndNavigateToNextMenus()
        {
            decimal expectedSalary = 60000m;

            mockInputHandler.Setup(ih => ih.PromptDecimal(It.IsAny<string>(), decimal.MinValue, decimal.MaxValue)).Returns(expectedSalary);

            mockLocaliser.Setup(l => l.Get(nameof(Strings.SalaryBeforeTaxUI_Message_Salary))).Returns(salaryText);

            ui.Display();
            
            mockUserInterface.Verify(ui => ui.Clear(), Times.Once);
            mockInputHandler.Verify(ih => ih.PromptDecimal(salaryText, decimal.MinValue, decimal.MaxValue), Times.Once);
            mockUserData.VerifySet(ud => ud.Salary = expectedSalary, Times.Once);
            mockUserData.VerifySet(ud => ud.IsSalaryBeforeTax = true, Times.Once);
            mockFlowController.Verify(fc => fc.NavigateTo(typeof(SalaryFrequencyMenu)), Times.Once);
        }
    }
}

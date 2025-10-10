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
        SalaryBeforeTaxUI ui;

        Mock<IUserInterface> mockUserInterface;
        Mock<IUIFlowController> mockFlowController;
        Mock<IInputHandler> mockInputHandler;
        Mock<IUserData> mockUserData;

        [TestInitialize]
        public void Setup()
        {
            mockUserInterface = new Mock<IUserInterface>();
            mockFlowController = new Mock<IUIFlowController>();
            mockInputHandler = new Mock<IInputHandler>();
            mockUserData = new Mock<IUserData>();

            ui = new SalaryBeforeTaxUI(mockUserInterface.Object, mockFlowController.Object, mockInputHandler.Object, mockUserData.Object);
        }

        [TestMethod]
        public void Display_ShouldPromptForSalaryAndNavigateToNextMenus()
        {
            decimal expectedSalary = 60000m;

            mockInputHandler.Setup(ih => ih.PromptDecimal(It.IsAny<string>(), decimal.MinValue, decimal.MaxValue)).Returns(expectedSalary);
            
            ui.Display();
            
            mockUserInterface.Verify(ui => ui.Clear(), Times.Once);
            mockInputHandler.Verify(ih => ih.PromptDecimal("Enter your salary before tax: ", decimal.MinValue, decimal.MaxValue), Times.Once);
            mockUserData.VerifySet(ud => ud.Salary = expectedSalary, Times.Once);
            mockUserData.VerifySet(ud => ud.IsSalaryBeforeTax = true, Times.Once);
            mockFlowController.Verify(fc => fc.NavigateTo(typeof(SalaryFrequencyMenu)), Times.Once);
        }
    }
}

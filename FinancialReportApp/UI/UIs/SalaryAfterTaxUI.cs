using FinancialReportApp.Systems;
using FinancialReportApp.UI.Menus;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI.UIs
{
    [Menu("Input salary after tax", typeof(InputSalaryMenu))]
    internal class SalaryAfterTaxUI : UIBase
    {
        private const string salaryText = "Enter your salary after tax: ";
        private const string taxText = "Enter the amount of tax paid: ";

        private readonly IUIFlowController flowController;
        private readonly IInputHandler inputHandler;
        private readonly IUserData userData;

        public SalaryAfterTaxUI(IUserInterface userInterface, IUIFlowController flowController, IInputHandler inputHandler, IUserData userData) : base(userInterface)
        {
            this.flowController = flowController;
            this.inputHandler = inputHandler;
            this.userData = userData;
        }

        public override void Display()
        {
            userInterface.Clear();
            userData.Salary = inputHandler.PromptDecimal(salaryText);
            userData.IsSalaryBeforeTax = false;

            flowController.NavigateTo(typeof(SalaryFrequencyMenu));

            userData.TaxPaid = inputHandler.PromptDecimal(taxText);
        }
    }
}

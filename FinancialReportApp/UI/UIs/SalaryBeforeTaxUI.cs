using FinancialReportApp.Systems;
using FinancialReportApp.UI.Menus;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    [Menu("Input salary before tax", typeof(InputSalaryMenu))]
    internal class SalaryBeforeTaxUI : UIBase
    {
        private const string salaryText = "Enter your salary before tax: ";
        
        private readonly IUIFlowController flowController;
        private readonly IInputHandler inputHandler;
        private readonly IUserData userData;

        public SalaryBeforeTaxUI(IUserInterface userInterface, IUIFlowController flowController, IInputHandler inputHandler, IUserData userData) : base(userInterface)
        {
            this.flowController = flowController;
            this.inputHandler = inputHandler;
            this.userData = userData;
        }

        public override void Display()
        {
            userInterface.Clear();
            userData.Salary = inputHandler.PromptDecimal(salaryText);
            userData.IsSalaryBeforeTax = true;

            flowController.NavigateTo(typeof(SalaryFrequencyMenu));

            flowController.NavigateTo(typeof(TaxCreditMenu));
        }
    }
}

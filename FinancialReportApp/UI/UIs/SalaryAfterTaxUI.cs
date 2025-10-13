using FinancialReportApp.Resources;
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
    [Menu(nameof(Strings.SalaryAfterTaxUI_Menu), typeof(InputSalaryMenu))]
    internal class SalaryAfterTaxUI : UIBase
    {
        private readonly IUIFlowController flowController;
        private readonly IInputHandler inputHandler;
        private readonly IUserData userData;

        public SalaryAfterTaxUI(IUserInterface userInterface, ILocaliser localiser, IUIFlowController flowController, IInputHandler inputHandler, IUserData userData) : base(userInterface, localiser)
        {
            this.flowController = flowController;
            this.inputHandler = inputHandler;
            this.userData = userData;
        }

        public override void Display()
        {
            userInterface.Clear();
            userData.Salary = GetSalary();
            userData.IsSalaryBeforeTax = false;

            flowController.NavigateTo(typeof(SalaryFrequencyMenu));

            userData.TaxPaid = GetTaxPaid();
        }

        private decimal GetSalary()
        {
            var message = localiser.Get(nameof(Strings.SalaryAfterTaxUI_Message_Salary));
            return inputHandler.PromptDecimal(message);
        }

        private decimal GetTaxPaid()
        {
            var message = localiser.Get(nameof(Strings.SalaryAfterTaxUI_Message_Tax));
            return inputHandler.PromptDecimal(message);
        }
    }
}

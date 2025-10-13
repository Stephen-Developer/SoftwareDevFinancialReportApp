using FinancialReportApp.Resources;
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
    [Menu(nameof(Strings.SalaryBeforeTaxUI_Menu), typeof(InputSalaryMenu))]
    internal class SalaryBeforeTaxUI : UIBase
    {
        private readonly IUIFlowController flowController;
        private readonly IInputHandler inputHandler;
        private readonly IUserData userData;

        public SalaryBeforeTaxUI(IUserInterface userInterface, ILocaliser localiser, IUIFlowController flowController, IInputHandler inputHandler, IUserData userData) : base(userInterface, localiser)
        {
            this.flowController = flowController;
            this.inputHandler = inputHandler;
            this.userData = userData;
        }

        public override void Display()
        {
            userInterface.Clear();
            var message = localiser.Get(nameof(Strings.SalaryBeforeTaxUI_Message_Salary));
            userData.Salary = inputHandler.PromptDecimal(message);
            userData.IsSalaryBeforeTax = true;

            flowController.NavigateTo(typeof(SalaryFrequencyMenu));
        }
    }
}

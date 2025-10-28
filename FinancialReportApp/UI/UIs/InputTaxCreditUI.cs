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
    [Menu(nameof(Strings.InputTaxCreditUI_Menu), typeof(TaxCreditMenu))]
    internal class InputTaxCreditUI : UIBase
    {
        private readonly IInputHandler inputHandler;
        private readonly IUserData userData;

        public InputTaxCreditUI(IUserInterface userInterface, ILocaliser localiser, IInputHandler inputHandler, IUserData userData) : base(userInterface, localiser)
        {
            this.inputHandler = inputHandler;
            this.userData = userData;
        }

        public override void Display()
        {
            userInterface.Clear();
            decimal credit = GetTaxCredit();

            userData.AddTaxCredit(credit);
        }

        private decimal GetTaxCredit()
        {
            var message = localiser.Get(nameof(Strings.InputTaxCreditUI_Prompt_Amount));
            return inputHandler.PromptDecimal(message, 0);
        }
    }
}

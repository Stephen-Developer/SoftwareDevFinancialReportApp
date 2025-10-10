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
    [Menu("Input tax credits", typeof(TaxCreditMenu))]
    internal class InputTaxCreditUI : UIBase
    {
        private readonly IInputHandler inputHandler;
        private readonly IUserData userData;

        public InputTaxCreditUI(IUserInterface userInterface, IInputHandler inputHandler, IUserData userData) : base(userInterface)
        {
            this.inputHandler = inputHandler;
            this.userData = userData;
        }

        public override void Display()
        {
            userInterface.Clear();
            decimal credit = inputHandler.PromptDecimal("Input tax credit/deductable: ", 0);

            userData.AddTaxCredit(credit);
        }
    }
}

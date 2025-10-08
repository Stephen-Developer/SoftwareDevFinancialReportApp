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

        public InputTaxCreditUI(IUserInterface userInterface, IInputHandler inputHandler) : base(userInterface)
        {
            this.inputHandler = inputHandler;
        }

        public override void Display()
        {
            userInterface.Clear();
            userInterface.Write("Input tax credit/deductable: ");
            decimal credit = inputHandler.PromptDecimal("Input tax credit/deductable: ", 0);

            TaxSystem.Instance.taxCredits.Add(credit);
        }
    }
}

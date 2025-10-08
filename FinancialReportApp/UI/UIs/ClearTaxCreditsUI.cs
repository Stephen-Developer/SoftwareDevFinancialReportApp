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
    [Menu("Clear Tax Credits", typeof(TaxCreditMenu))]
    internal class ClearTaxCreditsUI : UIBase
    {
        private readonly IInputHandler inputHandler;

        public ClearTaxCreditsUI(IUserInterface userInterface, IInputHandler inputHandler) : base(userInterface)
        {
            this.inputHandler = inputHandler;
        }

        public override void Display()
        {
            userInterface.Clear();
            var clearTaxCredits = inputHandler.PromptYesNo("Are you sure you want to clear all tax credits?");
            if (clearTaxCredits)
            {
                userInterface.WriteLine("All tax credits cleared.");
                TaxSystem.Instance.taxCredits.Clear();
            }
            else
            {
                userInterface.WriteLine("Tax credits not cleared.");
            }
            userInterface.WaitForKey();
        }
    }
}

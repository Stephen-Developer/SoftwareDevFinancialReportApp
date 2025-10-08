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
        private readonly IUserData userData;

        public ClearTaxCreditsUI(IUserInterface userInterface, IInputHandler inputHandler, IUserData userData) : base(userInterface)
        {
            this.inputHandler = inputHandler;
            this.userData = userData;
        }

        public override void Display()
        {
            userInterface.Clear();
            var clearTaxCredits = inputHandler.PromptYesNo("Are you sure you want to clear all tax credits?");
            if (clearTaxCredits)
            {
                userInterface.WriteLine("All tax credits cleared.");
                userData.ClearTaxCredits();
            }
            else
            {
                userInterface.WriteLine("Tax credits not cleared.");
            }
            userInterface.WaitForKey();
        }
    }
}

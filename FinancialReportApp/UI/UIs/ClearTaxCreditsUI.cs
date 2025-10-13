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
    [Menu(nameof(Strings.ClearCustomTaxUI_MenuAttribute), typeof(TaxCreditMenu))]
    internal class ClearTaxCreditsUI : UIBase
    {
        private readonly IInputHandler inputHandler;
        private readonly IUserData userData;

        public ClearTaxCreditsUI(IUserInterface userInterface, IInputHandler inputHandler, IUserData userData, ILocaliser localiser) : base(userInterface, localiser)
        {
            this.inputHandler = inputHandler;
            this.userData = userData;
        }

        public override void Display()
        {
            userInterface.Clear();
            var clearTaxCredits = ClearCustomTaxBracketsPrompt();
            if (clearTaxCredits)
            {
                userData.ClearTaxCredits();
                DisplayClearedMessage();
            }
            else
            {
                DisplayCancelledMessage();
            }
            userInterface.WaitForKey();
        }

        private bool ClearCustomTaxBracketsPrompt()
        {
            var message = localiser.Get(nameof(Strings.ClearTaxCreditsUI_Prompt));
            return inputHandler.PromptYesNo(message);
        }

        private void DisplayClearedMessage()
        {
            var message = localiser.Get(nameof(Strings.ClearTaxCreditsUI_Message_Cleared));
            userInterface.WriteLine(message);
        }

        private void DisplayCancelledMessage()
        {
            var message = localiser.Get(nameof(Strings.ClearTaxCreditsUI_Message_Cancelled));
            userInterface.WriteLine(message);
        }
    }
}

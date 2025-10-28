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
    [Menu(nameof(Strings.ClearCustomTaxUI_MenuAttribute), typeof(InputSalaryMenu))]
    internal class ClearCustomTaxUI : UIBase
    {
        private readonly IInputHandler inputHandler;
        private readonly IUserData userData;

        public ClearCustomTaxUI(IUserInterface userInterface, IInputHandler inputHandler, IUserData userData, ILocaliser localiser) : base(userInterface, localiser)
        {
            this.inputHandler = inputHandler;
            this.userData = userData;
        }

        public override void Display()
        {
            if(userData.CustomTaxBrackets.Count == 0)
            {
                return;
            }

            var clearTaxBrackets = ClearTaxBracketsPrompt();
            if (clearTaxBrackets)
            {
                userData.ClearTaxBrackets();
                DisplayClearedMessage();
            }
            else
            {
                DisplayCancelledMessage();  
            }
            userInterface.WaitForKey();
        }

        private bool ClearTaxBracketsPrompt()
        {
            var message = localiser.Get(nameof(Strings.ClearCustomTaxUI_Prompt));
            return inputHandler.PromptYesNo(message);
        }

        private void DisplayClearedMessage()
        {
            var message = localiser.Get(nameof(Strings.ClearCustomTaxUI_Message_Cleared));
            userInterface.WriteLine(message);
        }

        private void DisplayCancelledMessage()
        {
            var message = localiser.Get(nameof(Strings.ClearCustomTaxUI_Message_Cancelled));
            userInterface.WriteLine(message);
        }
    }
}

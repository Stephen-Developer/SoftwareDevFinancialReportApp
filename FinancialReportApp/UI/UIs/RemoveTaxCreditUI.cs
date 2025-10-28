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
    [Menu(nameof(Strings.RemoveTaxCreditUI_Menu), typeof(TaxCreditMenu))]
    internal class RemoveTaxCreditUI : UIBase
    {
        private readonly IInputHandler inputHandler;
        private readonly IUserData userData;

        public RemoveTaxCreditUI(IUserInterface userInterface, ILocaliser localiser, IInputHandler inputHandler, IUserData userData) : base(userInterface, localiser)
        {
            this.inputHandler = inputHandler;
            this.userData = userData;
        }

        public override void Display()
        {
            if (userData.TaxCredits.Count == 0)
            {
                NoCreditMessage();
                return;
            }

            DisplayCurrentCredits();

            int index = PromptGetIndexToRemove();

            RemoveIndex(index);
        }

        private void NoCreditMessage()
        {
            var message = localiser.Get(nameof(Strings.RemoveTaxCreditUI_Message_Nothing));
            userInterface.WriteLine(message);
            userInterface.WaitForKey();
        }

        private void DisplayCurrentCredits()
        {
            var message = localiser.Get(nameof(Strings.RemoveTaxCreditUI_Message_Current));
            userInterface.WriteLine(message);

            for(int i = 0; i < userData.TaxCredits.Count; i++)
            {
                var exp = userData.TaxCredits[i];
                userInterface.WriteLine($"{i + 1}. {exp}");
            }
        }

        private int PromptGetIndexToRemove()
        {
            var message = localiser.Get(nameof(Strings.RemoveTaxCreditUI_Prompt_Number));
            return inputHandler.PromptInt(message) - 1;
        }

        private void RemoveIndex(int index)
        {
            if (index >= 0 && index < userData.TaxCredits.Count)
            {
                userData.RemoveTaxCredit(index);
                var message = localiser.Get(nameof(Strings.RemoveTaxCreditUI_Message_Removed));
                userInterface.WriteLine(message);
            }
            else
            {
                var message = localiser.Get(nameof(Strings.RemoveTaxCreditUI_Message_Invalid));
                userInterface.WriteLine(message);
            }
            userInterface.WaitForKey();
        }
    }
}

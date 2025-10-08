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
    [Menu("Remove Tax Credit", typeof(TaxCreditMenu))]
    internal class RemoveTaxCreditUI : UIBase
    {
        private readonly IInputHandler inputHandler;
        private readonly IUserData userData;

        public RemoveTaxCreditUI(IUserInterface userInterface, IInputHandler inputHandler, IUserData userData) : base(userInterface)
        {
            this.inputHandler = inputHandler;
            this.userData = userData;
        }

        public override void Display()
        {
            var credits = userData.TaxCredits;
            if (credits.Count == 0)
            {
                userInterface.WriteLine("No credits to remove.");
                userInterface.WaitForKey();
                return;
            }
            userInterface.WriteLine("Current credits:");
            for (int i = 0; i < credits.Count; i++)
            {
                var exp = credits[i];
                userInterface.WriteLine($"{i + 1}. {exp})");
            }
            int index = inputHandler.PromptInt("Enter the number of the credits to remove: ") - 1;
            if (index >= 0 && index < credits.Count)
            {
                userData.RemoveTaxCredit(index);
                userInterface.WriteLine("Expense removed.");
            }
            else
            {
                userInterface.WriteLine("Invalid index.");
            }
            userInterface.WaitForKey();
        }
    }
}

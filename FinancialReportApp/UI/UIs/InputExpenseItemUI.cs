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
    [Menu(nameof(Strings.InputExpenseItem_Menu), typeof(InputExpensesMenu))]
    internal class InputExpenseItemUI : UIBase
    {
        private readonly IInputHandler inputHandler;
        private readonly IUserData userData;

        public InputExpenseItemUI(IUserInterface userInterface, ILocaliser localiser, IInputHandler inputHandler, IUserData userData) : base(userInterface, localiser)
        {
            this.inputHandler = inputHandler;
            this.userData = userData;
        }

        public override void Display()
        {
            var catagory = GetCatagory();
            var amount = GetAmount();
            var frequency = GetTimeFrequency();

            userData.AddExpense(catagory, amount, frequency);
        }

        private string GetCatagory()
        {
            var message = localiser.Get(nameof(Strings.InputExpenseItem_Prompt_Category));
            return inputHandler.PromptString(message);
        }

        private decimal GetAmount()
        {
            var message = localiser.Get(nameof(Strings.InputExpenseItem_Prompt_Amount));
            return inputHandler.PromptDecimal(message);
        }

        private TimeFrequency GetTimeFrequency()
        {
            var message = localiser.Get(nameof(Strings.InputExpenseItem_Prompt_Frequency));
            return inputHandler.PromptEnum<TimeFrequency>(message);
        }
    }
}

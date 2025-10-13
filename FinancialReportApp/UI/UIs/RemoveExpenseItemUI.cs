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
    [Menu(nameof(Strings.RemoveExpenseItemUI_Menu), typeof(InputExpensesMenu))]
    internal class RemoveExpenseItemUI : UIBase
    {
        private readonly IInputHandler inputHandler;
        private readonly IUserData userData ;

        public RemoveExpenseItemUI(IUserInterface userInterface, ILocaliser localiser, IInputHandler inputHandler, IUserData userData) : base(userInterface, localiser)
        {
            this.userInterface = userInterface;
            this.inputHandler = inputHandler;
            this.userData = userData;
        }

        public override void Display()
        {
            if (userData.Expenses.Count == 0)
            {
                NoExpensesMessage();
                return;
            }

            DisplayCurrentExpenses();

            int index = PromptGetIndexToRemove();
            RemoveIndex(index);
        }

        private void NoExpensesMessage()
        {
            var message = localiser.Get(nameof(Strings.RemoveExpenseItemUI_Message_Nothing));
            userInterface.WriteLine(message);
            userInterface.WaitForKey();
        }

        private void DisplayCurrentExpenses()
        {
            var message = localiser.Get(nameof(Strings.RemoveExpenseItemUI_Message_Current));
            userInterface.WriteLine(message);

            for (int i = 0; i < userData.Expenses.Count; i++)
            {
                var exp = userData.Expenses[i];
                userInterface.WriteLine($"{i + 1}. {exp.Category} - {exp.Amount} ({exp.Frequency})");
            }
        }

        private int PromptGetIndexToRemove()
        {
            var message = localiser.Get(nameof(Strings.RemoveExpenseItemUI_Prompt_Number));
            return inputHandler.PromptInt(message) - 1;
        }

        private void RemoveIndex(int index)
        {
            if (index >= 0 && index < userData.Expenses.Count)
            {
                userData.RemoveExpense(index);
                var message = localiser.Get(nameof(Strings.RemoveExpenseItemUI_Message_Removed));
                userInterface.WriteLine(message);
            }
            else
            {
                var message = localiser.Get(nameof(Strings.RemoveExpenseItemUI_Message_Invalid));
                userInterface.WriteLine(message);
            }
            userInterface.WaitForKey();
        }
    }
}

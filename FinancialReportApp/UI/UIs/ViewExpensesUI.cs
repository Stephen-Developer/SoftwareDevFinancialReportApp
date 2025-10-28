using FinancialReportApp.Resources;
using FinancialReportApp.Systems;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI.UIs
{
    [Menu(nameof(Strings.ViewExpensesUI_Menu), typeof(Menus.InputExpensesMenu))]
    internal class ViewExpensesUI : UIBase
    {
        private readonly IUserData userData;

        public ViewExpensesUI(IUserInterface userInterface, ILocaliser localiser, IUserData userData) : base(userInterface, localiser)
        {
            this.userData = userData;
        }

        public override void Display()
        {
            if (userData.Expenses.Count == 0)
            {
                NoExpensesMessage();
                return;
            }

            ShowCurrentExpenses();
        }

        private void NoExpensesMessage()
        {
            var message = localiser.Get(nameof(Strings.ViewExpensesUI_Message_Empty));
            userInterface.WriteLine(message);
            userInterface.WaitForKey();
        }

        private void ShowCurrentExpenses()
        {
            var message = localiser.Get(nameof(Strings.ViewExpensesUI_Message_Current));
            userInterface.WriteLine(message);
            foreach (var exp in userData.Expenses)
            {
                var expenseMessage = localiser.Get(nameof(Strings.ViewExpensesUI_Message_Expense), exp.Category, exp.Amount, exp.Frequency);
                userInterface.WriteLine(expenseMessage);
            }
            userInterface.WaitForKey();
        }
    }
}

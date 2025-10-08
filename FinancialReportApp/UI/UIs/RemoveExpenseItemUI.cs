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
    [Menu("Remove expense item", typeof(InputExpensesMenu))]
    internal class RemoveExpenseItemUI : UIBase
    {
        private readonly IInputHandler inputHandler;

        public RemoveExpenseItemUI(IUserInterface userInterface, IInputHandler inputHandler) : base(userInterface)
        {
            this.userInterface = userInterface;
            this.inputHandler = inputHandler;
        }

        public override void Display()
        {
            var expenses = UIController.Instance.UserInputData.Expenses;
            if (expenses.Count == 0)
            {
                userInterface.WriteLine("No expenses to remove.");
                userInterface.WaitForKey();
                return;
            }
            userInterface.WriteLine("Current Expenses:");
            for (int i = 0; i < expenses.Count; i++)
            {
                var exp = expenses[i];
                userInterface.WriteLine($"{i + 1}. {exp.Category} - {exp.Amount} ({exp.Frequency})");
            }
            int index = inputHandler.PromptInt("Enter the number of the expense to remove: ") - 1;
            if (index >= 0 && index < expenses.Count)
            {
                UIController.Instance.UserInputData.RemoveExpense(index);
                userInterface.WriteLine("Expense removed.");
            }
            else
            {
                userInterface.WriteLine("Invalid index.");
            }
        }
    }
}

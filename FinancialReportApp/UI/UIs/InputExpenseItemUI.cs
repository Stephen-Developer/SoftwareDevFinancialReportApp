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
    [Menu("Input expense item", typeof(InputExpensesMenu))]
    internal class InputExpenseItemUI : UIBase
    {
        private readonly IInputHandler inputHandler;

        public InputExpenseItemUI(IUserInterface userInterface, IInputHandler inputHandler) : base(userInterface)
        {
            this.inputHandler = inputHandler;
        }

        public override void Display()
        {
            string catagory = inputHandler.PromptString("Enter expense category: ");
            decimal amount = inputHandler.PromptDecimal("Enter expense amount: ");
            TimeFrequency frequency = inputHandler.PromptEnum<TimeFrequency>("Enter expense frequency (e.g., Monthly, Weekly): ");

            UIController.Instance.UserInputData.AddExpense(catagory, amount, frequency);
        }
    }
}

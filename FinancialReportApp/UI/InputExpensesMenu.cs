using FinancialReportApp.Systems;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    internal class InputExpensesMenu : Menu
    {
        private const string startText = "Input Expenses Menu - Please select an option:";
        private const string endText = "Option: ";
        private const string errorText = "Invalid option number.";

        private static InputExpensesMenu _instance;

        public static InputExpensesMenu Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InputExpensesMenu();
                }
                return _instance;
            }
        }

        private InputExpensesMenu() : base(startText, endText, errorText)
        {
            AddMenuAction("Input Expense Item", InputExpenseItem);
            AddMenuAction("Remove Expense Item", RemoveExpenseItem);
            AddMenuAction("View all Expenses", ViewAllExpenses);
            AddMenuAction(BACK, () => exit = true);
        }

        private void InputExpenseItem()
        {
            string catagory = InputHandler.PromtString("Enter expense category: ");
            decimal amount = InputHandler.PromtDecimal("Enter expense amount: ");
            TimeFrequency frequency = InputHandler.PromtEnum<TimeFrequency>("Enter expense frequency (e.g., Monthly, Weekly): ");

            Expense expense = new Expense(catagory, amount, frequency);

            UIController.Instance.UserInputData.AddExpense(expense);
        }

        private void RemoveExpenseItem()
        {
            var expenses = UIController.Instance.UserInputData.Expenses;
            if (expenses.Count == 0)
            {
                Console.WriteLine("No expenses to remove.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Current Expenses:");
            for (int i = 0; i < expenses.Count; i++)
            {
                var exp = expenses[i];
                Console.WriteLine($"{i + 1}. {exp.Category} - {exp.Amount} ({exp.Frequency})");
            }
            int index = InputHandler.PromtInt("Enter the number of the expense to remove: ") - 1;
            if (index >= 0 && index < expenses.Count)
            {
                UIController.Instance.UserInputData.RemoveExpense(index);
                Console.WriteLine("Expense removed.");
            }
            else
            {
                Console.WriteLine("Invalid index.");
            }
        }

        private void ViewAllExpenses()
        {
            var expenses = UIController.Instance.UserInputData.Expenses;
            if (expenses.Count == 0)
            {
                Console.WriteLine("No expenses recorded.");
            }
            else
            {
                Console.WriteLine("Current Expenses:");
                foreach (var exp in expenses)
                {
                    Console.WriteLine($"{exp.Category} - {exp.Amount} ({exp.Frequency})");
                }
            }
            Console.ReadLine();
        }
    }
}

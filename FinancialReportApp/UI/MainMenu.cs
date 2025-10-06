using FinancialReportApp.Systems;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    internal class MainMenu : Menu
    {
        private const string startText = "Please select an option:";
        private const string endText = "Option: ";
        private const string errorText = "Invalid option number.";

        private static MainMenu _instance;

        public static MainMenu Instance 
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MainMenu();
                }
                return _instance;
            }
        }

        public MainMenu() : base(startText, endText, errorText)
        {
            AddMenuAction("Input Salary", InputSalary);
            AddMenuAction("Input Expenses", InputExpenses);
            AddMenuAction("Input Custom Tax Brackets", InputCustomTaxBrackets);
            AddMenuAction("Generate Report", GenerateReport);
            AddMenuAction("Exit", () => exit = true);
            AddMenuAction("Add Debug Expenses", AddDebugValues);
            AddMenuAction("Add Debug Tax Brackets", AddDebugTaxBrackets);
        }

        private void InputSalary()
        {
            InputSalaryMenu.Instance.Display();
        }

        private void InputExpenses()
        {
            InputExpensesMenu.Instance.Display();
        }

        private void GenerateReport()
        {
            ReportUI.Instance.Display();
        }

        private void InputCustomTaxBrackets()
        {
            InputCustomTaxMenu.Instance.Display();
        }

        private void AddDebugValues()
        {
            UIController.Instance.UserInputData.Salary = 100000;
            UIController.Instance.UserInputData.SalaryFrequency = TimeFrequency.Yearly;
            UIController.Instance.UserInputData.AddExpense("Car", 10000, TimeFrequency.OneTime);
            UIController.Instance.UserInputData.AddExpense("Car", 20, TimeFrequency.Weekly);
            UIController.Instance.UserInputData.AddExpense("Bills", 200, TimeFrequency.Monthly);
            UIController.Instance.UserInputData.AddExpense("Food", 60, TimeFrequency.Weekly);
            UIController.Instance.UserInputData.AddExpense("Rent", 1500, TimeFrequency.Monthly);
            TaxSystem.Instance.taxCredits.Add(1000);
            TaxSystem.Instance.taxCredits.Add(500);
        }

        private void AddDebugTaxBrackets()
        {
            TaxSystem.Instance.AddTaxBracket(0, 20, 0.2m);
            TaxSystem.Instance.AddTaxBracket(21, 40, 0.3m);
            TaxSystem.Instance.AddTaxBracket(41, 60, 0.4m);
            TaxSystem.Instance.AddTaxBracket(61, null, 0.5m);
        }
    }
}

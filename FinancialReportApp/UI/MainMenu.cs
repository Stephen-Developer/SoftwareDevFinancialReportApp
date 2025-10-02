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
            AddMenuAction("Generate Report", GenerateReport);
            AddMenuAction("Exit", () => exit = true);
            AddMenuAction("Debug", AddDebugValues);
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

        private void AddDebugValues()
        {
            UIController.Instance.UserInputData.Salary = 100000;
            UIController.Instance.UserInputData.SalaryFrequency = TimeFrequency.Yearly;
            UIController.Instance.UserInputData.AddExpense(new Expense("Car", 10000, TimeFrequency.OneTime));
            UIController.Instance.UserInputData.AddExpense(new Expense("Car", 20, TimeFrequency.Weekly));
            UIController.Instance.UserInputData.AddExpense(new Expense("Bills", 200, TimeFrequency.Monthly));
            UIController.Instance.UserInputData.AddExpense(new Expense("Food", 60, TimeFrequency.Weekly));
        }
    }
}

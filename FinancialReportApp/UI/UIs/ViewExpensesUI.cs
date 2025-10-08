using FinancialReportApp.Systems;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI.UIs
{
    [Menu("View all expenses", typeof(Menus.InputExpensesMenu))]
    internal class ViewExpensesUI : UIBase
    {
        private readonly IUserData userData;

        public ViewExpensesUI(IUserInterface userInterface, IUserData userData) : base(userInterface)
        {
            this.userData = userData;
        }

        public override void Display()
        {
            var expenses = userData.Expenses;
            if (expenses.Count == 0)
            {
                userInterface.WriteLine("No expenses recorded.");
            }
            else
            {
                userInterface.WriteLine("Current Expenses:");
                foreach (var exp in expenses)
                {
                    userInterface.WriteLine($"{exp.Category} - {exp.Amount} ({exp.Frequency})");
                }
            }
            userInterface.WaitForKey();
        }
    }
}

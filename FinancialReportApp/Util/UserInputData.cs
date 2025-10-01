using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Util
{
    internal class UserInputData
    {
        private List<Expense> expenses = new List<Expense>();

        public decimal Salary { get; set; }
        public bool IsSalaryBeforeTax { get; set; }
        public decimal TaxPaid { get; set; }
        public TimeFrequency SalaryFrequency { get; set; }
        public List<Expense> Expenses { get => expenses; }

        public void AddExpense(Expense expense)
        {
            expenses.Add(expense);
        }

        public void ClearExpenses()
        {
            expenses.Clear();
        }

        public void RemoveExpense(int index)
        {
            if (index >= 0 && index < expenses.Count)
            {
                expenses.RemoveAt(index);
            }
        }

        public void ClearAllData()
        {
            Salary = 0;
            IsSalaryBeforeTax = true;
            TaxPaid = 0;
            SalaryFrequency = TimeFrequency.Monthly;
            ClearExpenses();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Util
{
    public interface IUserData
    {
        //Salary
        decimal Salary { get; set; }
        bool IsSalaryBeforeTax { get; set; }
        decimal TaxPaid { get; set; }
        TimeFrequency SalaryFrequency { get; set; }

        //Expenses
        IReadOnlyList<Expense> Expenses { get; }
        void AddExpense(string category, decimal amount, TimeFrequency frequency);
        void ClearExpenses();
        void RemoveExpense(int index);

        //Tax and tax credits
        bool UseCustomTax { get; set; }
        IReadOnlyList<TaxBracket> CustomTaxBrackets { get; }
        IReadOnlyList<decimal> TaxCredits { get; }
        void AddTaxBracket(decimal lowerBoundary, decimal? upperBoundary, decimal rate);
        void ClearTaxBrackets();
        void AddTaxCredit(decimal amount);
        void RemoveTaxCredit(int index);
        void ClearTaxCredits();

        //Utility
        void ClearAllData();
    }

    public class UserData : IUserData
    {
        private readonly List<Expense> expenses = new List<Expense>();
        private readonly List<TaxBracket> customTaxBrackets = new List<TaxBracket>();
        private readonly List<decimal> taxCredits = new List<decimal>();

        public decimal Salary { get; set; }
        public bool IsSalaryBeforeTax { get; set; }
        public decimal TaxPaid { get; set; }
        public TimeFrequency SalaryFrequency { get; set; }
        public IReadOnlyList<Expense> Expenses => expenses.AsReadOnly();

        public bool UseCustomTax { get; set; }

        public IReadOnlyList<TaxBracket> CustomTaxBrackets => customTaxBrackets.AsReadOnly();

        public IReadOnlyList<decimal> TaxCredits => taxCredits.AsReadOnly();

        public void AddExpense(string category, decimal amount, TimeFrequency frequency)
        {
            var expense = new Expense(category, amount, frequency);
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
            SalaryFrequency = default(TimeFrequency);
            ClearExpenses();
            UseCustomTax = false;
            ClearTaxBrackets();
            ClearTaxCredits();
        }

        public void AddTaxBracket(decimal lowerBoundary, decimal? upperBoundary, decimal rate)
        {
            customTaxBrackets.Add(new TaxBracket(lowerBoundary, upperBoundary, rate));
        }

        public void ClearTaxBrackets() => customTaxBrackets.Clear();

        public void AddTaxCredit(decimal amount) => taxCredits.Add(amount);

        public void RemoveTaxCredit(int index)
        {
            if (index >= 0 && index < taxCredits.Count)
            {
                taxCredits.RemoveAt(index);
            }
        }

        public void ClearTaxCredits() => taxCredits.Clear();
    }
}

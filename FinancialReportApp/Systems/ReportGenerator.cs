using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Systems
{
    internal class ReportGenerator
    {
        private static ReportGenerator _instance;
        public static ReportGenerator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ReportGenerator();
                }
                return _instance;
            }
        }

        private ReportData reportData = new ReportData();

        public void ProcessData()
        {
            var userData = UIController.Instance.UserInputData;

            reportData.AnnualSalary = CalculateAnnualAmount(userData.Salary, userData.SalaryFrequency);
            reportData.MonthlySalary = reportData.AnnualSalary / 12;
            reportData.WeeklySalary = reportData.AnnualSalary / 52;

            foreach (var expense in userData.Expenses)
            {
                decimal annualExpense = CalculateAnnualAmount(expense.Amount, expense.Frequency);
                decimal monthlyExpense = annualExpense / 12;
                decimal weeklyExpense = annualExpense / 52;

                reportData.AnnualExpenses[expense.Category] = reportData.AnnualExpenses.GetValueOrDefault(expense.Category) + annualExpense;
            }
        }

        private decimal CalculateAnnualAmount(decimal salary, Util.TimeFrequency frequency)
        {
            return frequency switch
            {
                Util.TimeFrequency.OneTime => salary,
                Util.TimeFrequency.Yearly => salary,
                Util.TimeFrequency.Monthly => salary * 12,
                Util.TimeFrequency.Weekly => salary * 52,
                _ => throw new ArgumentOutOfRangeException(nameof(frequency), "Invalid time frequency"),
            };
        }

        public string GenerateReport()
        {
            StringBuilder report = new StringBuilder();

            report.AppendLine("=====Financial Report=====");
            report.AppendLine("-----Annual Summary-----");
            report.AppendLine($"Annual Salary before tax: {reportData.AnnualSalary:C}");
            report.AppendLine($"Annual tax: ");
            report.AppendLine($"Total annual expense: {reportData.TotalAnnualExpense:C}");
            report.AppendLine($"Annual expenses:");
            foreach(var expenseKV in reportData.AnnualExpenses) {
                report.AppendLine($" - {expenseKV.Key}: {expenseKV.Value}");
            }

            report.AppendLine("-----Monthly Summary-----");
            report.AppendLine($"Monthly Salary before tax: {reportData.MonthlySalary:C}");
            report.AppendLine($"Monthly tax: ");
            report.AppendLine($"Total monthly expense: {reportData.TotalMonthlyExpense:C}");
            report.AppendLine($"Monthly expenses:");
            foreach (var expenseKV in reportData.MonthlyExpenses)
            {
                report.AppendLine($" - {expenseKV.Key}: {expenseKV.Value}");
            }

            report.AppendLine("-----Weekly Summary-----");
            report.AppendLine($"Weekly Salary before tax: {reportData.WeeklySalary:C}");
            report.AppendLine($"Weekly tax: ");
            report.AppendLine($"Total weekly expense: {reportData.TotalWeeklyExpense:C}");
            report.AppendLine($"Weekly expenses:");
            foreach (var expenseKV in reportData.WeeklyExpenses)
            {
                report.AppendLine($" - {expenseKV.Key}: {expenseKV.Value}");
            }

            return report.ToString();
        }
    }

    internal class ReportData
    {
        private Dictionary<string, decimal> annualExpenses = new Dictionary<string, decimal>();
        private Dictionary<string, decimal> monthlyExpenses = new Dictionary<string, decimal>();
        private Dictionary<string, decimal> weeklyExpenses = new Dictionary<string, decimal>();

        public decimal AnnualSalary { get; set; }
        public decimal MonthlySalary { get; set; }
        public decimal WeeklySalary { get; set; }

        public decimal AnnualTax { get; set; }
        public decimal MonthlyTax { get; set; }
        public decimal WeeklyTax { get; set; }

        public decimal TotalAnnualExpense { get; set; }
        public decimal TotalMonthlyExpense { get; set; }
        public decimal TotalWeeklyExpense { get; set; }


        public Dictionary<string, decimal> AnnualExpenses { get => annualExpenses;}
        public Dictionary<string, decimal> MonthlyExpenses { get => monthlyExpenses;}
        public Dictionary<string, decimal> WeeklyExpenses { get => weeklyExpenses;}
    }

    internal class CalculatedExpense
    {
        public string Category { get; set; }
        public decimal Amount { get; set; }
    }
}

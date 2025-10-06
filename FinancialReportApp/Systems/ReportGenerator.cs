using FinancialReportApp.Util;
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

            reportData.AnnualTax = CalculateTax(reportData.AnnualSalary);
            reportData.MonthlyTax = reportData.AnnualTax / 12;
            reportData.WeeklyTax = reportData.AnnualTax / 52;

            reportData.AnnualTaxCredits = TaxSystem.Instance.taxCredits.Sum();
            reportData.MonthlyTaxCredits = reportData.AnnualTaxCredits / 12;
            reportData.WeeklyTaxCredits = reportData.AnnualTaxCredits / 52;

            reportData.NetAnnual = reportData.AnnualSalary - reportData.AnnualTax + reportData.AnnualTaxCredits;
            reportData.NetMonthly = reportData.MonthlySalary - reportData.MonthlyTax + reportData.MonthlyTaxCredits;
            reportData.NetWeekly = reportData.WeeklySalary - reportData.WeeklyTax + reportData.WeeklyTaxCredits;

            foreach (var expense in userData.Expenses)
            {
                decimal annualExpense = CalculateAnnualAmount(expense.Amount, expense.Frequency);
                decimal monthlyExpense = annualExpense / 12;
                decimal weeklyExpense = annualExpense / 52;

                reportData.AnnualExpenses[expense.Category] = reportData.AnnualExpenses.GetValueOrDefault(expense.Category) + annualExpense;
            }

            foreach(var kv in reportData.AnnualExpenses)
            {
                reportData.TotalAnnualExpense += kv.Value;
                reportData.MonthlyExpenses[kv.Key] = kv.Value / 12;
                reportData.WeeklyExpenses[kv.Key] = kv.Value / 52;
            }
            reportData.TotalMonthlyExpense = reportData.TotalAnnualExpense / 12;
            reportData.TotalWeeklyExpense = reportData.TotalAnnualExpense / 52;
        }

        private decimal CalculateTax(decimal yearlyIncome)
        {
            if(TaxSystem.Instance.useCustomTax)
            {
                return 0;
            }

            decimal taxOwed = 0;

            foreach (var bracket in TaxSystem.Instance.defaultTaxBracketList)
            {
                if(yearlyIncome > bracket.LowerBoundary)
                {
                    decimal incomeInBracket;
                    if(bracket.UpperBoundary.HasValue && yearlyIncome > bracket.UpperBoundary.Value)
                    {
                        incomeInBracket = Math.Min(yearlyIncome, bracket.UpperBoundary.Value) - bracket.LowerBoundary;
                    }
                    else
                    {
                        incomeInBracket = yearlyIncome - bracket.LowerBoundary;
                    }
                    taxOwed += incomeInBracket * (bracket.Rate / 100);
                }
                else
                {
                    break;
                }
            }

            return taxOwed;
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
            report.AppendLine($"Annual tax: {reportData.AnnualTax:C}");
            report.AppendLine($"Annual tax credits: {reportData.AnnualTaxCredits:C}");
            report.AppendLine($"Annual net: {reportData.NetAnnual:C}");
            report.AppendLine($"Total annual expense: {reportData.TotalAnnualExpense:C}");
            report.AppendLine($"Annual expenses:");
            foreach(var expenseKV in reportData.AnnualExpenses) {
                report.AppendLine($" - {expenseKV.Key}: {expenseKV.Value:C}");
            }
            report.AppendLine($"Annual after expenses: {reportData.NetAnnual - reportData.TotalAnnualExpense:C}");

            report.AppendLine("-----Monthly Summary-----");
            report.AppendLine($"Monthly Salary before tax: {reportData.MonthlySalary:C}");
            report.AppendLine($"Monthly tax: {reportData.MonthlyTax:C}");
            report.AppendLine($"Monthly tax credits: {reportData.MonthlyTaxCredits:C}");
            report.AppendLine($"Monthly net: {reportData.NetMonthly:C}");
            report.AppendLine($"Total monthly expense: {reportData.TotalMonthlyExpense:C}");
            report.AppendLine($"Monthly expenses:");
            foreach (var expenseKV in reportData.MonthlyExpenses)
            {
                report.AppendLine($" - {expenseKV.Key}: {expenseKV.Value:C}");
            }
            report.AppendLine($"Monthly after expenses: {reportData.NetMonthly - reportData.TotalMonthlyExpense:C}");

            report.AppendLine("-----Weekly Summary-----");
            report.AppendLine($"Weekly Salary before tax: {reportData.WeeklySalary:C}");
            report.AppendLine($"Weekly tax: {reportData.WeeklyTax:C}");
            report.AppendLine($"Weekly tax credits: {reportData.WeeklyTaxCredits:C}");
            report.AppendLine($"Weekly net: {reportData.NetMonthly:C}");
            report.AppendLine($"Total weekly expense: {reportData.TotalWeeklyExpense:C}");
            report.AppendLine($"Weekly expenses:");
            foreach (var expenseKV in reportData.WeeklyExpenses)
            {
                report.AppendLine($" - {expenseKV.Key}: {expenseKV.Value:C}");
            }
            report.AppendLine($"Weekly after expenses: {reportData.NetWeekly - reportData.TotalWeeklyExpense:C}");

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

        public decimal AnnualTaxCredits { get; set; }
        public decimal MonthlyTaxCredits { get; set; }
        public decimal WeeklyTaxCredits { get; set; }

        public decimal NetAnnual { get; set; }
        public decimal NetMonthly { get; set; }
        public decimal NetWeekly { get; set; }

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

using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Systems
{
    public interface IReportGenerator
    {
        void ProcessData();
        string GenerateReport();
    }

    public class ReportGenerator : IReportGenerator
    {
        private readonly IUserData userData;
        private readonly ITaxSystem taxSystem;

        private ReportData reportData = new ReportData();

        // Expose ReportData for testing purposes
        internal ReportData ReportData => reportData;

        public ReportGenerator(IUserData userData, ITaxSystem taxSystem)
        {
            this.userData = userData;
            this.taxSystem = taxSystem;
        }

        public void ProcessData()
        {
            reportData.Salary = new TimeValue(CalculateAnnualAmount(userData.Salary, userData.SalaryFrequency));
            reportData.Tax = new TimeValue( taxSystem.CalculateTax(reportData.Salary.Annual));
            reportData.TaxCredits = new TimeValue(userData.TaxCredits.Sum());
            reportData.Net = new TimeValue(reportData.Salary.Annual - reportData.Tax.Annual + reportData.TaxCredits.Annual);

            foreach (var expense in userData.Expenses)
            {
                decimal annualExpense = CalculateAnnualAmount(expense.Amount, expense.Frequency);

                if (!reportData.Expenses.TryGetValue(expense.Category, out var value))
                {
                    value = new TimeValue(0);
                }
                value.AddAnnual(annualExpense);
                value.ComputeMonthlyWeekly();
                reportData.Expenses[expense.Category] = value;
            }

            reportData.TotalExpenses = new TimeValue(0);
            foreach (var kv in reportData.Expenses)
            {
                reportData.TotalExpenses.AddAnnual(kv.Value.Annual);
            }
            reportData.TotalExpenses.ComputeMonthlyWeekly();
        }

        private decimal CalculateAnnualAmount(decimal salary, Util.TimeFrequency frequency)
        {
            return frequency switch
            {
                TimeFrequency.OneTime => salary,
                TimeFrequency.Yearly => salary,
                TimeFrequency.Monthly => salary * 12,
                TimeFrequency.Weekly => salary * 52,
                _ => throw new ArgumentOutOfRangeException(nameof(frequency), "Invalid time frequency"),
            };
        }

        public string GenerateReport()
        {
            StringBuilder report = new StringBuilder();

            foreach (var freq in new[] { TimeFrequency.Yearly, TimeFrequency.Monthly, TimeFrequency.Weekly })
            {
                AppendTimeSummary(report, freq, reportData);
            }

            return report.ToString();
        }

        private void AppendTimeSummary(StringBuilder stringBuilder, TimeFrequency time, ReportData data)
        {
            var label = time.ToString();

            stringBuilder.AppendLine($"-----{label} Summary-----");
            stringBuilder.AppendLine($"{label} Salary before tax: {data.Salary.GetValue(time):C}");
            stringBuilder.AppendLine($"{label} tax: {data.Tax.GetValue(time):C}");
            stringBuilder.AppendLine($"{label} tax credits: {data.TaxCredits.GetValue(time):C}");
            stringBuilder.AppendLine($"{label} net: {data.Net.GetValue(time):C}");
            stringBuilder.AppendLine($"{label} expenses:");
            foreach (var expenseKV in data.Expenses)
            {
                stringBuilder.AppendLine($" - {expenseKV.Key}: {expenseKV.Value.GetValue(time):C}");
            }
            stringBuilder.AppendLine($"Total {label} expenses: {data.TotalExpenses.GetValue(time):C}");
            stringBuilder.AppendLine($"{label} after expenses: {data.Net.GetValue(time) - data.TotalExpenses.GetValue(time):C}");
        }
    }

    internal class TimeValue
    {
        public decimal Annual { get; set; }
        public decimal Monthly { get; set; }
        public decimal Weekly { get; set; }

        public TimeValue(decimal annual)
        {
            Annual = annual;
            Monthly = annual / 12;
            Weekly = annual / 52;
        }

        public void AddAnnual(decimal value)
        {
            Annual += value;
        }

        public void ComputeMonthlyWeekly()
        {
            Monthly = Annual / 12;
            Weekly = Annual / 52;
        }

        public decimal GetValue(TimeFrequency frequency)
        {
            return frequency switch
            {
                TimeFrequency.OneTime => Annual,
                TimeFrequency.Yearly => Annual,
                TimeFrequency.Monthly => Monthly,
                TimeFrequency.Weekly => Weekly,
                _ => throw new ArgumentOutOfRangeException(nameof(frequency), "Invalid time frequency"),
            };
        }
    }

    internal class ReportData
    {
        private Dictionary<string, TimeValue> expenses = new Dictionary<string, TimeValue>();

        public TimeValue Salary { get; set; }
        public TimeValue Tax { get; set; }
        public TimeValue TaxCredits { get; set; }
        public TimeValue Net { get; set; }
        public TimeValue TotalExpenses { get; set; }

        public Dictionary<string, TimeValue> Expenses { get => expenses; }
    }
}

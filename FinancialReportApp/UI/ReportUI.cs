using FinancialReportApp.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    internal class ReportUI : UI
    {
        private static ReportUI _instance;

        public static ReportUI Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ReportUI();
                }
                return _instance;
            }
        }

        public override void Display()
        {
            ReportGenerator.Instance.ProcessData();
            Console.WriteLine(ReportGenerator.Instance.GenerateReport());
            Console.ReadLine();
            /*StringBuilder report = new StringBuilder();

            report.AppendLine("=====Financial Report=====");
            report.AppendLine($"Salary: {UIController.Instance.UserInputData.Salary:C}");
            report.AppendLine($"Salary Type: {(UIController.Instance.UserInputData.IsSalaryBeforeTax ? "Before Tax" : "After Tax")}");
            report.AppendLine($"Tax Paid: {UIController.Instance.UserInputData.TaxPaid:C}");
            report.AppendLine($"Salary Frequency: {UIController.Instance.UserInputData.SalaryFrequency}");
            report.AppendLine($"Expenses:");
            foreach (var expense in UIController.Instance.UserInputData.Expenses)
            {
                report.AppendLine($" - {expense.Catagory}: {expense.Amount:C} ({expense.Frequency})");
            }

            Console.WriteLine(report.ToString());
            Console.ReadLine();*/
        }
    }
}

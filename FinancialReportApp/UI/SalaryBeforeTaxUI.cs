using FinancialReportApp.Systems;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    internal class SalaryBeforeTaxUI : UI
    {
        private static SalaryBeforeTaxUI? _instance;

        public static SalaryBeforeTaxUI Instance 
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SalaryBeforeTaxUI();
                }
                return _instance;
            }
        }

        public override void Display()
        {
            Console.Clear();
            UIController.Instance.UserInputData.Salary = InputHandler.PromtDecimal("Enter your salary before tax: ");
            UIController.Instance.UserInputData.IsSalaryBeforeTax = true;

            SalaryFrequencyMenu.Instance.Display();

            TaxCreditMenu.Instance.Display();
        }
    }
}

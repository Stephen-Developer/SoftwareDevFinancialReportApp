using FinancialReportApp.Systems;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    internal class SalaryAfterTaxUI : UIBase
    {
        private const string salaryText = "Enter your salary after tax: ";
        private const string taxText = "Enter the amount of tax paid: ";

        public SalaryAfterTaxUI(IUserInterface userInterface) : base(userInterface)
        {

        }

        public override void Display()
        {
            Console.Clear();
            UIController.Instance.UserInputData.Salary = InputHandler.PromtDecimal(salaryText);
            UIController.Instance.UserInputData.IsSalaryBeforeTax = false;

            //SalaryFrequencyMenu.Instance.Display();

            UIController.Instance.UserInputData.TaxPaid = InputHandler.PromtDecimal(taxText);
        }
    }
}

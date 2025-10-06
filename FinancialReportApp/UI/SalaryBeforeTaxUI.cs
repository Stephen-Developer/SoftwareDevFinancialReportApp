using FinancialReportApp.Systems;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    internal class SalaryBeforeTaxUI : UIBase
    {
        public SalaryBeforeTaxUI(IUserInterface userInterface) : base(userInterface)
        {

        }

        public override void Display()
        {
            Console.Clear();
            UIController.Instance.UserInputData.Salary = InputHandler.PromtDecimal("Enter your salary before tax: ");
            UIController.Instance.UserInputData.IsSalaryBeforeTax = true;

            //SalaryFrequencyMenu.Instance.Display();

            //TaxCreditMenu.Instance.Display();
        }
    }
}

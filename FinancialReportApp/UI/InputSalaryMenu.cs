using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    internal class InputSalaryMenu : Menu
    {
        private const string startText = "Input Salary Menu - Please select an option:";
        private const string endText = "Option: ";
        private const string errorText = "Invalid option number.";

        private static InputSalaryMenu _instance;
        public static InputSalaryMenu Instance 
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InputSalaryMenu();
                }
                return _instance;
            }
        }

        public InputSalaryMenu() : base(startText, endText, errorText)
        {
            AddMenuAction("Input Salary Before Tax", InputSalaryBeforeTax);
            AddMenuAction("Input Salary After Tax", InputSalaryAfterTax);
            AddMenuAction(BACK, () => exit = true);
        }

        private void InputSalaryBeforeTax()
        {
            SalaryBeforeTaxUI.Instance.Display();
        }

        private void InputSalaryAfterTax()
        {
            SalaryAfterTaxUI.Instance.Display();
        }
    }
}

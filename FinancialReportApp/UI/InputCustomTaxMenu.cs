using FinancialReportApp.Systems;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    internal class InputCustomTaxMenu : Menu
    {
        private const string startText = "Input Custom Tax Brackets Menu";
        private const string endText = "Option: ";
        private const string errorText = "Invalid option number.";

        private static InputCustomTaxMenu _instance;

        public static InputCustomTaxMenu Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new InputCustomTaxMenu();
                }
                return _instance;
            }
        }

        private InputCustomTaxMenu() : base(startText, endText, errorText)
        {
            AddMenuAction("Input Custom Tax Brackets", InputCustomTax);
            AddMenuAction("Clear Custom Tax Brackets", ClearCustomTax);
            AddMenuAction("View Current Custom Tax Brackets", ViewCustomTaxBrackets);
            AddMenuAction(BACK, () => exit = true);
        }

        private void InputCustomTax()
        {
            ClearCustomTax();
            var exit = false;
            while(!exit)
            {
                Console.Clear();
                var lowerLimit = TaxSystem.Instance.customTaxBracketList.Count == 0 ? 0 : TaxSystem.Instance.customTaxBracketList.Last<TaxBracket>().UpperBoundary;
                Console.WriteLine($"Custom Tax Bracket starting point: {lowerLimit}");
                var upperLimit = InputHandler.PromtNullableDecimal("Enter upper boundary (or leave blank for no upper limit): ");
                var rate = InputHandler.PromtDecimal("Enter tax rate (as a percentage): ", 0, 100);
            }
        }

        private void ClearCustomTax()
        {

        }

        private void ViewCustomTaxBrackets()
        {

        }
    }
}

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

        public InputCustomTaxMenu(IUserInterface userInterface) : base(userInterface, startText, endText, errorText)
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
                decimal lowerLimit = TaxSystem.Instance.customTaxBracketList.Count == 0 ? 0 : TaxSystem.Instance.customTaxBracketList.Last<TaxBracket>().UpperBoundary.Value;
                Console.WriteLine($"Custom Tax Bracket starting point: {lowerLimit}");
                var upperLimit = InputHandler.PromtNullableDecimal("Enter upper boundary (or leave blank for no upper limit): ", lowerLimit);
                var rate = InputHandler.PromtDecimal("Enter tax rate (as a percentage): ", 0, 100);

                var TaxBracket = new TaxBracket(lowerLimit, upperLimit, rate / 100);
                TaxSystem.Instance.customTaxBracketList.Add(TaxBracket);

                if (TaxSystem.Instance.customTaxBracketList.LastOrDefault().UpperBoundary == null)
                {
                    Console.WriteLine("The last tax bracket has no upper limit. Unable to add more brackets.");
                    Console.ReadKey();
                    break;
                }
            }
        }

        private void ClearCustomTax()
        {
            var clearTaxBrackets = InputHandler.PromtYesNo("Are you sure you want to clear all custom tax brackets?");
            if (clearTaxBrackets)
            {
                TaxSystem.Instance.customTaxBracketList.Clear();
                Console.WriteLine("Custom tax brackets cleared.");
            }
            else
            {
                Console.WriteLine("Operation cancelled.");
            }
            Console.ReadKey();
        }

        private void ViewCustomTaxBrackets()
        {
            foreach(var taxBracket in TaxSystem.Instance.customTaxBracketList)
            {
                Console.WriteLine($"Range: {taxBracket.LowerBoundary} - {taxBracket.UpperBoundary}. Rate: {taxBracket.Rate}");
            }
            Console.ReadKey();
        }
    }
}

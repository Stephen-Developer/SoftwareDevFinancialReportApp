using FinancialReportApp.Systems;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    internal class TaxCreditMenu : Menu
    {
        private const string startText = "Tax Credit Menu - Please select an option:";
        private const string endText = "Option: ";
        private const string errorText = "Invalid option number.";

        private static TaxCreditMenu _instance;

        public TaxCreditMenu() : base(startText, endText, errorText)
        {
            AddMenuAction("Input Tax Credit", InputTaxCredit);
            AddMenuAction("Remove Tax Credit", RemoveTaxCredit);
            AddMenuAction("Clear Tax Credits", ClearTaxCredits);
            AddMenuAction("View Current Tax Credits", ViewTaxCreditAmount);
            AddMenuAction(BACK, () => exit = true);
        }

        public static TaxCreditMenu Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TaxCreditMenu();
                }
                return _instance;
            }
        }

        private void InputTaxCredit()
        {
            Console.Clear();
            Console.Write("Input tax credit/deductable: ");
            decimal credit = InputHandler.PromtDecimal("Input tax credit/deductable: ", 0);

            TaxSystem.Instance.taxCredits.Add(credit);
        }

        private void RemoveTaxCredit()
        {
            var credits = TaxSystem.Instance.taxCredits;
            if (credits.Count == 0)
            {
                Console.WriteLine("No credits to remove.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Current credits:");
            for (int i = 0; i < credits.Count; i++)
            {
                var exp = credits[i];
                Console.WriteLine($"{i + 1}. {exp})");
            }
            int index = InputHandler.PromtInt("Enter the number of the credits to remove: ") - 1;
            if (index >= 0 && index < credits.Count)
            {
                TaxSystem.Instance.taxCredits.RemoveAt(index);
                Console.WriteLine("Expense removed.");
            }
            else
            {
                Console.WriteLine("Invalid index.");
            }
        }

        private void ClearTaxCredits()
        {
            Console.Clear();
            var clearTaxCredits = InputHandler.PromtYesNo("Are you sure you want to clear all tax credits?");
            if(clearTaxCredits)
            {
                Console.WriteLine("All tax credits cleared.");
                TaxSystem.Instance.taxCredits.Clear();
            }
            else
            {
                Console.WriteLine("Tax credits not cleared.");
            }
            Console.ReadLine();
        }

        private void ViewTaxCreditAmount()
        {
            Console.Clear();
            decimal totalCredits = TaxSystem.Instance.taxCredits.Sum();
            Console.WriteLine($"Total Tax Credits: {totalCredits:C}");
            foreach(var credit in TaxSystem.Instance.taxCredits)
            {
                Console.WriteLine($" - {credit:C}");
            }
            Console.ReadLine();
        }
    }
}

using FinancialReportApp.Systems;
using FinancialReportApp.UI.Menus;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI.UIs
{
    [Menu("View All Tax Credits", typeof(TaxCreditMenu))]
    internal class ViewTaxCreditsUI : UIBase
    {
        public ViewTaxCreditsUI(IUserInterface userInterface) : base(userInterface)
        {
        }

        public override void Display()
        {
            userInterface.Clear();
            decimal totalCredits = TaxSystem.Instance.taxCredits.Sum();
            userInterface.WriteLine($"Total Tax Credits: {totalCredits:C}");
            foreach (var credit in TaxSystem.Instance.taxCredits)
            {
                userInterface.WriteLine($" - {credit:C}");
            }
            userInterface.WaitForKey();
        }
    }
}

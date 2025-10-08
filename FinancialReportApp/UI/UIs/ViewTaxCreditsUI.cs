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
        private IUserData userData;

        public ViewTaxCreditsUI(IUserInterface userInterface, IUserData userData) : base(userInterface)
        {
            this.userData = userData;
        }

        public override void Display()
        {
            userInterface.Clear();
            decimal totalCredits = userData.TaxCredits.Sum();
            userInterface.WriteLine($"Total Tax Credits: {totalCredits:C}");
            foreach (var credit in userData.TaxCredits)
            {
                userInterface.WriteLine($" - {credit:C}");
            }
            userInterface.WaitForKey();
        }
    }
}

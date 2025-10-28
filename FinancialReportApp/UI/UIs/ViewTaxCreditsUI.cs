using FinancialReportApp.Resources;
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
    [Menu(nameof(Strings.ViewTaxCreditsUI_Menu), typeof(TaxCreditMenu))]
    internal class ViewTaxCreditsUI : UIBase
    {
        private IUserData userData;

        public ViewTaxCreditsUI(IUserInterface userInterface, ILocaliser localiser, IUserData userData) : base(userInterface, localiser)
        {
            this.userData = userData;
        }

        public override void Display()
        {
            userInterface.Clear();
            decimal totalCredits = userData.TaxCredits.Sum();
            var message = localiser.Get(nameof(Strings.ViewTaxCreditsUI_Message_Total), totalCredits);
            userInterface.WriteLine(message);
            foreach (var credit in userData.TaxCredits)
            {
                var creditMessage = localiser.Get(nameof(Strings.ViewTaxCreditsUI_Message_Credit), credit);
                userInterface.WriteLine(creditMessage);
            }
            userInterface.WaitForKey();
        }
    }
}

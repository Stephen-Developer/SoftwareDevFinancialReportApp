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
    [Menu(nameof(Strings.ViewCustomTaxUI_Menu), typeof(InputCustomTaxMenu))]
    internal class ViewCustomTaxUI : UIBase
    {
        private IUserData userData;

        public ViewCustomTaxUI(IUserInterface userInterface, ILocaliser localiser, IUserData userData) : base(userInterface, localiser)
        {
            this.userData = userData;
        }

        public override void Display()
        {
            foreach (var taxBracket in userData.CustomTaxBrackets)
            {
                var message = localiser.Get(nameof(Strings.ViewCustomTaxUI_Message_Bracket), taxBracket.LowerBoundary, taxBracket.UpperBoundary, taxBracket.Rate);
                userInterface.WriteLine(message);
            }
            userInterface.WaitForKey();
        }
    }
}

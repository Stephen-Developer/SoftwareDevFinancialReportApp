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
    [Menu("View Custom Tax Brackets", typeof(InputCustomTaxMenu))]
    internal class ViewCustomTaxUI : UIBase
    {
        private IUserData userData;

        public ViewCustomTaxUI(IUserInterface userInterface, IUserData userData) : base(userInterface)
        {
            this.userData = userData;
        }

        public override void Display()
        {
            foreach (var taxBracket in userData.CustomTaxBrackets)
            {
                userInterface.WriteLine($"Range: {taxBracket.LowerBoundary} - {taxBracket.UpperBoundary}. Rate: {taxBracket.Rate}");
            }
            userInterface.WaitForKey();
        }
    }
}

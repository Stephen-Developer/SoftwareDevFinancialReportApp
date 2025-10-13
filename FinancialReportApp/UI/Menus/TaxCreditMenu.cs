using FinancialReportApp.Resources;
using FinancialReportApp.Systems;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI.Menus
{
    [Menu(nameof(Strings.TaxCreditMenu_Menu), typeof(InputSalaryMenu))]
    internal class TaxCreditMenu : Menu
    {
        public TaxCreditMenu(IUserInterface userInterface, ILocaliser localiser, IUIRegistry registry) 
            : base(
                  userInterface,
                  localiser, 
                  localiser.Get(nameof(Strings.TaxCreditMenu_Message_Start)),
                  null, 
                  null)
        {
            
        }
    }
}

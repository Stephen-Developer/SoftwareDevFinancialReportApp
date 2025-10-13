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
    [Menu(nameof(Strings.InputExpensesMenu_Menu), typeof(MainMenu))]
    internal class InputExpensesMenu : Menu
    {
        public InputExpensesMenu(IUserInterface userInterface, ILocaliser localiser, IUIRegistry registry) 
            : base(
                  userInterface, 
                  localiser, 
                  localiser.Get(nameof(Strings.InputExpensesMenu_Message_Start)), 
                  null, 
                  null)
        {
            
        }
    }
}

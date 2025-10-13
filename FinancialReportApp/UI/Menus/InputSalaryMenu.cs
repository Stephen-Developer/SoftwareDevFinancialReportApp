using FinancialReportApp.Resources;
using FinancialReportApp.Util;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI.Menus
{
    [Menu(nameof(Strings.InputSalaryMenu_Menu), typeof(MainMenu))]
    internal class InputSalaryMenu : Menu
    {
        public InputSalaryMenu(IUserInterface userInterface, ILocaliser localiser, IUIRegistry registry) 
            : base(
                  userInterface, 
                  localiser, 
                  localiser.Get(nameof(Strings.InputSalaryMenu_Message_Start)), 
                  null, 
                  null)
        {

        }
    }
}

using FinancialReportApp.Resources;
using FinancialReportApp.Systems;
using FinancialReportApp.Util;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI.Menus
{
    [Menu(nameof(Strings.InputCustomTaxMenu_Menu), typeof(MainMenu))]
    internal class InputCustomTaxMenu : Menu
    {
        public InputCustomTaxMenu(IUserInterface userInterface, ILocaliser localiser) 
            : base(
                  userInterface, 
                  localiser, 
                  localiser.Get(nameof(Strings.InputCustomTaxMenu_Message_Start)), 
                  null, 
                  null)
        {
            
        }
    }
}

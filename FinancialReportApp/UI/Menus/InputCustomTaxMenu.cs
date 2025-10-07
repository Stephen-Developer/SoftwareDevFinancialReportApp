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
    [Menu("Input custom tax brackets", typeof(MainMenu))]
    internal class InputCustomTaxMenu : Menu
    {
        private const string startText = "Input Custom Tax Brackets Menu";
        private const string endText = "Option: ";
        private const string errorText = "Invalid option number.";

        public InputCustomTaxMenu(IUserInterface userInterface, IUIRegistry registry) : base(userInterface, startText, endText, errorText)
        {
            int maxOrder = registry.GetAllUIs()
                .Where(d => d.ParentType == typeof(InputCustomTaxMenu)).Count();
            
            AddMenuAction(BACK, Exit, ++maxOrder);
        }
    }
}

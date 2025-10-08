using FinancialReportApp.Util;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI.Menus
{
    [Menu("Input Salary", typeof(MainMenu))]
    internal class InputSalaryMenu : Menu
    {
        private const string startText = "Input Salary Menu - Please select an option:";
        private const string endText = "Option: ";
        private const string errorText = "Invalid option number.";

        public InputSalaryMenu(IUserInterface userInterface, IUIRegistry registry) : base(userInterface, startText, endText, errorText)
        {
            int maxOrder = registry.GetMaxOrderForParent<InputSalaryMenu>();
            AddMenuAction(BACK, Exit, ++maxOrder);
        }
    }
}

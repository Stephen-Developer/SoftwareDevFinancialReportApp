using FinancialReportApp.Systems;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI.Menus
{
    [Menu("Input expenses", typeof(MainMenu))]
    internal class InputExpensesMenu : Menu
    {
        private const string startText = "Input Expenses Menu - Please select an option:";
        private const string endText = "Option: ";
        private const string errorText = "Invalid option number.";

        public InputExpensesMenu(IUserInterface userInterface, IUIRegistry registry) : base(userInterface, startText, endText, errorText)
        {
            var maxOrder = registry.GetMaxOrderForParent<InputExpensesMenu>();
            AddMenuAction(BACK, Exit, ++maxOrder);
        }
    }
}

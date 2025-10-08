using FinancialReportApp.Systems;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI.Menus
{
    [Menu("Input tax credits", typeof(InputSalaryMenu))]
    internal class TaxCreditMenu : Menu
    {
        private const string startText = "Tax Credit Menu - Please select an option:";
        private const string endText = "Option: ";
        private const string errorText = "Invalid option number.";

        public TaxCreditMenu(IUserInterface userInterface, IUIRegistry registry) : base(userInterface, startText, endText, errorText)
        {
            int maxOrder = registry.GetMaxOrderForParent<TaxCreditMenu>();
            AddMenuAction(BACK, Exit, ++maxOrder);
        }
    }
}

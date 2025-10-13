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
    [Menu(nameof(Strings.SalaryFrequencyMenu_Menu))]
    internal class SalaryFrequencyMenu : Menu
    {
        private readonly IUserData userData;

        public SalaryFrequencyMenu(IUserInterface userInterface, ILocaliser localiser, IUserData userData)
            : base(
                  userInterface, 
                  localiser, 
                  localiser.Get(Strings.SalaryFrequencyMenu_Message_Start), 
                  null, 
                  null)
        {
            this.userData = userData;

            AddMenuAction(TimeFrequency.Weekly.ToString(), () => SelectFrequency(TimeFrequency.Weekly));
            AddMenuAction(TimeFrequency.Monthly.ToString(), () => SelectFrequency(TimeFrequency.Monthly));
            AddMenuAction(TimeFrequency.Yearly.ToString(), () => SelectFrequency(TimeFrequency.Yearly));
        }

        private void SelectFrequency(TimeFrequency frequency)
        {
            userData.SalaryFrequency = frequency;
            exit = true;
        }
    }
}

using FinancialReportApp.Systems;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI.Menus
{
    [Menu("Select Salary Frequency", default)]
    internal class SalaryFrequencyMenu : Menu
    {
        private const string startText = "Select Salary Frequency:";
        private const string endText = "Option: ";
        private const string errorText = "Invalid option number.";

        public SalaryFrequencyMenu(IUserInterface userInterface) : base(userInterface,startText, endText, errorText)
        {
            AddMenuAction(TimeFrequency.Weekly.ToString(), () => SelectFrequency(TimeFrequency.Weekly));
            AddMenuAction(TimeFrequency.Monthly.ToString(), () => SelectFrequency(TimeFrequency.Monthly));
            AddMenuAction(TimeFrequency.Yearly.ToString(), () => SelectFrequency(TimeFrequency.Yearly));
        }

        private void SelectFrequency(TimeFrequency frequency)
        {
            UIController.Instance.UserInputData.SalaryFrequency = frequency;
            exit = true;
        }
    }
}

using FinancialReportApp.Systems;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    internal class SalaryFrequencyMenu : Menu
    {
        private const string startText = "Select Salary Frequency:";
        private const string endText = "Option: ";
        private const string errorText = "Invalid option number.";

        private static SalaryFrequencyMenu? _instance;

        public static SalaryFrequencyMenu Instance 
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SalaryFrequencyMenu();
                }
                return _instance;
            }
        }

        public SalaryFrequencyMenu() : base(startText, endText, errorText)
        {
            AddMenuAction(1, TimeFrequency.Weekly.ToString(), () => SelectFrequency(TimeFrequency.Weekly));
            AddMenuAction(2, TimeFrequency.Monthly.ToString(), () => SelectFrequency(TimeFrequency.Monthly));
            AddMenuAction(3, TimeFrequency.Yearly.ToString(), () => SelectFrequency(TimeFrequency.Yearly));
        }

        private void SelectFrequency(TimeFrequency frequency)
        {
            UIController.Instance.UserInputData.SalaryFrequency = frequency;
            exit = true;
        }
    }
}

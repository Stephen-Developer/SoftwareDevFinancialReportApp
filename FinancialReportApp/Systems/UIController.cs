using FinancialReportApp.UI;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Systems
{
    internal class UIController
    {
        private static UIController? _instance;

        private UserInputData userInputData;

        public UserInputData UserInputData { get => userInputData; }

        public static UIController Instance 
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UIController();
                    _instance.userInputData = new UserInputData();
                }
                return _instance;
            }
        }
    }
}

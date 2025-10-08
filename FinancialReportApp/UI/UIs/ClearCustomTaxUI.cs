using FinancialReportApp.Systems;
using FinancialReportApp.UI.Menus;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI.UIs
{
    [Menu("Clear custom tax brackets", typeof(InputSalaryMenu))]
    internal class ClearCustomTaxUI : UIBase
    {
        private readonly IInputHandler inputHandler;
        private readonly IUserData userData;

        public ClearCustomTaxUI(IUserInterface userInterface, IInputHandler inputHandler, IUserData userData) : base(userInterface)
        {
            this.inputHandler = inputHandler;
            this.userData = userData;
        }

        public override void Display()
        {
            if(userData.CustomTaxBrackets.Count == 0)
            {
                return;
            }

            var clearTaxBrackets = inputHandler.PromptYesNo("Are you sure you want to clear all custom tax brackets?");
            if (clearTaxBrackets)
            {
                userData.ClearTaxBrackets();
                userInterface.WriteLine("Custom tax brackets cleared.");
            }
            else
            {
                userInterface.WriteLine("Operation cancelled.");
            }
            userInterface.WaitForKey();
        }
    }
}

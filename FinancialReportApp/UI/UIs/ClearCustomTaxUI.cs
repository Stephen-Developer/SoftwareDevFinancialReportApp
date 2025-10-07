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

        public ClearCustomTaxUI(IUserInterface userInterface, IInputHandler inputHandler) : base(userInterface)
        {
            this.inputHandler = inputHandler;
        }

        public override void Display()
        {
            if(TaxSystem.Instance.customTaxBracketList.Count == 0)
            {
                return;
            }

            var clearTaxBrackets = inputHandler.PromptYesNo("Are you sure you want to clear all custom tax brackets?");
            if (clearTaxBrackets)
            {
                TaxSystem.Instance.customTaxBracketList.Clear();
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

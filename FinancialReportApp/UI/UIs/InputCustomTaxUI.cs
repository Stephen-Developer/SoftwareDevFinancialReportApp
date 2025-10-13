using FinancialReportApp.Resources;
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
    [Menu(nameof(Strings.InputCustomTaxUI_Menu), typeof(InputCustomTaxMenu))]
    internal class InputCustomTaxUI : UIBase
    {
        private readonly IInputHandler inputHandler;
        private readonly IUserData userData;
        private readonly IUIFlowController flowController;

        public InputCustomTaxUI(IUserInterface userInterface, ILocaliser localiser, IInputHandler inputHandler, IUserData userData, IUIFlowController flowController) : base(userInterface, localiser)
        {
            this.inputHandler = inputHandler;
            this.userData = userData;
            this.flowController = flowController;
        }
        
        public override void Display()
        {
            flowController.NavigateTo(typeof(ClearCustomTaxUI));
            
            if(userData.CustomTaxBrackets.Count > 0)
                return;

            while (true)
            {
                userInterface.Clear();
                decimal lowerLimit = GetLowerLimit();
                
                WriteLowerLimitPrompt(lowerLimit);
                
                var upperLimit = GetUpperLimit(lowerLimit);
                var rate = GetTaxRate();

                userData.AddTaxBracket(lowerLimit, upperLimit, rate / 100);

                if (upperLimit == null)
                {
                    EndBracketInput();
                    break;
                }
            }
        }

        private decimal GetLowerLimit()
        {
            return userData.CustomTaxBrackets.LastOrDefault()?.UpperBoundary ?? 0;
        }

        private void WriteLowerLimitPrompt(decimal lowerLimit)
        {
            string message = localiser.Get(nameof(Strings.InputCustomTaxUI_Message_Lower), lowerLimit);
            userInterface.WriteLine(message);
        }

        private decimal? GetUpperLimit(decimal lowerLimit)
        {
            string message = localiser.Get(nameof(Strings.InputCustomTaxUI_Message_Upper));
            return inputHandler.PromptNullableDecimal(message, lowerLimit);
        }

        private decimal GetTaxRate()
        {
            string message = localiser.Get(nameof(Strings.InputCustomTaxUI_Message_Rate));
            return inputHandler.PromptDecimal(message, 0, 100);
        }

        private void EndBracketInput()
        {
            string message = localiser.Get(nameof(Strings.InputCustomTaxUI_Message_End));
            userInterface.WriteLine(message);
            userInterface.WaitForKey();
        }
    }
}

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
    [Menu("Input custom tax brackets", typeof(InputCustomTaxMenu))]
    internal class InputCustomTaxUI : UIBase
    {
        private readonly IUIRegistry registry;
        private readonly IInputHandler inputHandler;
        private readonly IUserData userData;

        public InputCustomTaxUI(IUIRegistry registry, IUserInterface userInterface, IInputHandler inputHandler, IUserData userData) : base(userInterface)
        {
            this.registry = registry;
            this.inputHandler = inputHandler;
            this.userData = userData;
        }

        public override void Display()
        {
            registry.Get<ClearCustomTaxUI>().Display();
            var exit = false;

            while (!exit)
            {
                userInterface.Clear();
                decimal lowerLimit = userData.CustomTaxBrackets.Count == 0 ? 0 : userData.CustomTaxBrackets.Last().UpperBoundary.Value;
                userInterface.WriteLine($"Custom Tax Bracket starting point: {lowerLimit}");
                var upperLimit = inputHandler.PromptNullableDecimal("Enter upper boundary (or leave blank for no upper limit): ", lowerLimit);
                var rate = inputHandler.PromptDecimal("Enter tax rate (as a percentage): ", 0, 100);

                userData.AddTaxBracket(lowerLimit, upperLimit, rate / 100);

                if (userData.CustomTaxBrackets.LastOrDefault().UpperBoundary == null)
                {
                    userInterface.WriteLine("The last tax bracket has no upper limit. Unable to add more brackets.");
                    userInterface.WaitForKey();
                    break;
                }
            }
        }
    }
}

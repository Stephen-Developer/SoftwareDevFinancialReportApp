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

        public InputCustomTaxUI(IUIRegistry registry, IUserInterface userInterface, IInputHandler inputHandler) : base(userInterface)
        {
            this.registry = registry;
            this.inputHandler = inputHandler;
        }

        public override void Display()
        {
            registry.Get<ClearCustomTaxUI>().Display();
            var exit = false;

            while (!exit)
            {
                userInterface.Clear();
                decimal lowerLimit = TaxSystem.Instance.customTaxBracketList.Count == 0 ? 0 : TaxSystem.Instance.customTaxBracketList.Last().UpperBoundary.Value;
                userInterface.WriteLine($"Custom Tax Bracket starting point: {lowerLimit}");
                var upperLimit = inputHandler.PromptNullableDecimal("Enter upper boundary (or leave blank for no upper limit): ", lowerLimit);
                var rate = inputHandler.PromptDecimal("Enter tax rate (as a percentage): ", 0, 100);

                var TaxBracket = new TaxBracket(lowerLimit, upperLimit, rate / 100);
                TaxSystem.Instance.customTaxBracketList.Add(TaxBracket);

                if (TaxSystem.Instance.customTaxBracketList.LastOrDefault().UpperBoundary == null)
                {
                    userInterface.WriteLine("The last tax bracket has no upper limit. Unable to add more brackets.");
                    userInterface.WaitForKey();
                    break;
                }
            }
        }
    }
}

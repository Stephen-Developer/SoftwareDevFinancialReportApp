using FinancialReportApp.Systems;
using FinancialReportApp.UI.Menus;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    [Menu("Input salary before tax", typeof(InputSalaryMenu))]
    internal class SalaryBeforeTaxUI : UIBase
    {
        private const string salaryText = "Enter your salary before tax: ";
        
        private readonly IUIRegistry registry;
        private readonly IInputHandler inputHandler;

        public SalaryBeforeTaxUI(IUserInterface userInterface, IUIRegistry registry, IInputHandler inputHandler) : base(userInterface)
        {
            this.registry = registry;
            this.inputHandler = inputHandler;
        }

        public override void Display()
        {
            userInterface.Clear();
            UIController.Instance.UserInputData.Salary = inputHandler.PromptDecimal(salaryText);
            UIController.Instance.UserInputData.IsSalaryBeforeTax = true;

            registry.Get<SalaryFrequencyMenu>().Display();

            registry.Get<TaxCreditMenu>().Display();
        }
    }
}

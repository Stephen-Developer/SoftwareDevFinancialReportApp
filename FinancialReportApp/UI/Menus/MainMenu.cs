using FinancialReportApp.Resources;
using FinancialReportApp.Systems;
using FinancialReportApp.Util;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI.Menus
{
    [Menu(nameof(Strings.MainMenu_Menu))]
    internal class MainMenu : Menu
    {
#if DEBUG
        private readonly IUserData userData;

        public MainMenu(IUserInterface userInterface, ILocaliser localiser, IUIRegistry registry, IUserData userData)
            : base(
                  userInterface, 
                  localiser, 
                  null, 
                  null, 
                  null)
        {
            this.userData = userData;
            int maxOrder = registry.GetMaxOrderForParent<MainMenu>();
            AddMenuAction("Add Debug Values", AddDebugValues, ++maxOrder);
            AddMenuAction("Add Debug Tax Brackets", AddDebugTaxBrackets, ++maxOrder);
        }

        private void AddDebugValues()
        {
            userData.Salary = 100000;
            userData.SalaryFrequency = TimeFrequency.Yearly;
            userData.AddExpense("Car", 10000, TimeFrequency.OneTime);
            userData.AddExpense("Car", 20, TimeFrequency.Weekly);
            userData.AddExpense("Bills", 200, TimeFrequency.Monthly);
            userData.AddExpense("Food", 60, TimeFrequency.Weekly);
            userData.AddExpense("Rent", 1500, TimeFrequency.Monthly);
            userData.AddTaxCredit(1000);
            userData.AddTaxCredit(500);
        }

        private void AddDebugTaxBrackets()
        {
            userData.AddTaxBracket(0, 20, 0.2m);
            userData.AddTaxBracket(21, 40, 0.3m);
            userData.AddTaxBracket(41, 60, 0.4m);
            userData.AddTaxBracket(61, null, 0.5m);
        }
#else
        public MainMenu(IUserInterface userInterface, IUIRegistry registry, ILocaliser localiser) 
            : base(userInterface, localiser, null, null, null)
        {
            
        }
#endif
    }
}

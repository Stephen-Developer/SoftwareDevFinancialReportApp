using FinancialReportApp.Systems;
using FinancialReportApp.UI;
using FinancialReportApp.Util;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialReportApp
{
    internal class Program
    {
        /*
        The project you will be prototyping as a command line application in order to flesh out the 
        functionality will be a personal income calculator. The application should be simple and intuitive to 
        use with all menu choices clearly labelled and requiring no instruction. The application should 
        provide the following functionality:
            • Allow users to input their salary (weekly/monthly/yearly) before or after tax
                o Before tax: allow users to input their tax credits and handle the tax deductions 
                  then output take home pay based on current Irish tax brackets
                o After tax: allow users to optionally specify their tax for calculation into final report
            • Allow users to input any expenses they may have by specifying a category tag and amount 
            • Display a final ‘report’ detailing the users starting salary, all expenses broken down in 
              categories and a final take home pay showing how much they earn weekly, monthly and 
              yearly
        Nice to have extras/Advanced features:
            • The ability to specify custom tax brackets to handle potential non-Irish tax residents to use 
            the application
            • The ability to save the final ‘report’ into a text file
        */

        /*
         * Input salary
         *  - Before tax
         *      - Input tax credits
         *          - What info do I need here?
         *      - Input tax deductions
         *          - What info do I need here?
         *  - After tax
         *      - Input tax (optional)
         * Input expenses (category tag and amount)
         *  - Do I need to ask for frequency? (one-time, weekly, monthly, yearly)
         * Generate report
         *  - What is the format of the report?
         *  - Show on console
         *  - Output to file (optional)
         * Custom tax brackets (optional)
         *  - What info do I need here?
         * Exit
         */

        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            //Core systems
            services.AddSingleton<IUserInterface, ConsoleUserInterface>();

            //UI
            services.AddSingleton<MainMenu>();
            services.AddSingleton<InputSalaryMenu>();
            services.AddSingleton<InputExpensesMenu>();
            services.AddSingleton<InputCustomTaxMenu>();
            services.AddSingleton<ReportUI>();
            services.AddSingleton<SalaryBeforeTaxUI>();
            services.AddSingleton<SalaryAfterTaxUI>();
            services.AddSingleton<TaxCreditMenu>();

            //Registry
            services.AddSingleton<IUIRegistry, UIRegistry>();
            var provider = services.BuildServiceProvider();
            var registry = provider.GetRequiredService<IUIRegistry>();

            //Set up hierarchy
            //MainMenu
            registry.RegisterMenu("Input Salary", typeof(InputSalaryMenu), typeof(MainMenu));
            registry.RegisterMenu("Input Expenses", typeof(InputExpensesMenu), typeof(MainMenu));
            registry.RegisterMenu("Input Custom Tax Brackets", typeof(InputCustomTaxMenu), typeof(MainMenu));
            registry.RegisterMenu("Generate Report", typeof(ReportUI), typeof(MainMenu));

            //InputSalaryMenu
            registry.RegisterMenu("Input Salary Before Tax", typeof(SalaryBeforeTaxUI), typeof(InputSalaryMenu));
            registry.RegisterMenu("Input Salary After Tax", typeof(SalaryAfterTaxUI), typeof(InputSalaryMenu));
            registry.RegisterMenu("Input Tax Credits", typeof(TaxCreditMenu), typeof(InputSalaryMenu));

            //Build hierarchy
            registry.BuildMenuHierarchy();

            var mainMenu = provider.GetRequiredService<MainMenu>();
            mainMenu.Display();
        }
    }
}

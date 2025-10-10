using FinancialReportApp.Systems;
using FinancialReportApp.UI;
using FinancialReportApp.UI.Menus;
using FinancialReportApp.UI.UIs;
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

            // Core systems
            services.AddSingleton<IUserInterface, ConsoleUserInterface>();
            services.AddSingleton<IUIRegistry, UIRegistry>();
            services.AddSingleton<IInputHandler, InputHandler>();
            services.AddSingleton<IUIFlowController, UIFlowController>();
            services.AddSingleton<IUserData, UserData>();
            services.AddSingleton<ITaxSystem, TaxSystem>();
            services.AddSingleton<IReportGenerator, ReportGenerator>();
            services.AddSingleton<IFileService, FileService>();

            UIRegistration.RegisterUIs(services);

            //Registry
            var provider = services.BuildServiceProvider();
            var registry = provider.GetRequiredService<IUIRegistry>();

            //Set up hierarchy
            UIRegistration.RegisterMenuHierarchy(registry);

            //Build hierarchy
            registry.BuildMenuHierarchy();

            var flow = provider.GetRequiredService<IUIFlowController>();
            flow.NavigateTo(typeof(MainMenu));
        }
    }
}

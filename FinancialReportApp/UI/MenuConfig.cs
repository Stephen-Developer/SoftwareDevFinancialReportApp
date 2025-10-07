using FinancialReportApp.UI.Menus;
using FinancialReportApp.UI.UIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    public static class MenuConfig
    {
        public static readonly List<(Type type, Type? parent)> Hierarchy = new()
        {
            (typeof(MainMenu), null),
            //Main Menu
            (typeof(InputSalaryMenu), typeof(MainMenu)),
            (typeof(InputExpensesMenu), typeof(MainMenu)),
            (typeof(InputCustomTaxMenu), typeof(MainMenu)),
            (typeof(ReportUI), typeof(MainMenu)),

            //InputSalaryMenu
            (typeof(SalaryBeforeTaxUI), typeof(InputSalaryMenu)),
            (typeof(SalaryAfterTaxUI), typeof(InputSalaryMenu)),
            (typeof(TaxCreditMenu), typeof(InputSalaryMenu)),

            //InputExpensesMenu

            //InputCustomTaxMenu
            (typeof(InputCustomTaxUI), typeof(InputCustomTaxMenu)),
            (typeof(ClearCustomTaxUI), typeof(InputCustomTaxMenu)),
            (typeof(ViewCustomTaxUI), typeof(InputCustomTaxMenu)),

            //InputTaxCreditMenu

            //SalaryFrequencyMenu

        };
    }
}

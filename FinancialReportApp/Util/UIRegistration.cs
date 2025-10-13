using FinancialReportApp.UI;
using FinancialReportApp.UI.UIs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Util
{
    public static class UIRegistration
    {
        public static void RegisterUIs(IServiceCollection services)
        {
            // Discover all menus with attributes
            var uiTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.GetCustomAttributes(typeof(MenuAttribute), false).Any())
                .ToList();

            // Register all UIs
            foreach (var type in uiTypes)
                services.AddSingleton(type);
        }

        public static void RegisterMenuHierarchy(IUIRegistry registry)
        {
            var groups = MenuConfig.Hierarchy
                .GroupBy(entry => entry.parent)
                .ToList();

            foreach (var group in groups)
            {
                int order = 1;
                foreach (var (type, parent) in group)
                {
                    var attr = (MenuAttribute)type.GetCustomAttributes(typeof(MenuAttribute), false).First();

                    var displayName = attr.GetLocalizedLabel();
                    
                    registry.RegisterMenu(displayName, type, order++, parent);
                }
            }
        }
    }
}

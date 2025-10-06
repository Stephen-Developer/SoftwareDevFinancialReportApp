using FinancialReportApp.UI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Util
{
    public interface IUIRegistry
    {
        T Get<T>() where T : UI.UIBase;
        IEnumerable<UIDescriptor> GetAllMenus();    
    }

    public class UIRegistry : IUIRegistry
    {
        private readonly IServiceProvider provider;
        private readonly Dictionary<Type, List<UIDescriptor>> menuMap = new();

        public UIRegistry(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public T Get<T>() where T : UI.UIBase
        {
            return provider.GetRequiredService<T>();
        }

        public IEnumerable<UIDescriptor> GetAllMenus()
        {
            return GetAllMenusForType(typeof(MainMenu));
        }

        public IEnumerable<UIDescriptor> GetAllMenusForType(Type type)
        {
            if (menuMap.TryGetValue(type, out var list))
                return list.OrderBy(m => m.Order);

            return Enumerable.Empty<UIDescriptor>();
        }

        public void RegisterMenu(string label, Type menuType, int order = 0, Type parentMenuType = null)
        {
            var menuInstance = (UI.UIBase)provider.GetRequiredService(menuType);
            var descriptor = new UIDescriptor(label, menuInstance, order);

            var key = parentMenuType ?? typeof(MainMenu);

            if (!menuMap.ContainsKey(key))
                menuMap[key] = new List<UIDescriptor>();

            menuMap[key].Add(descriptor);
        }
    }
}

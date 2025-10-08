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
        IEnumerable<UIDescriptor> GetAllUIs();    
        IEnumerable<UIDescriptor> GetAllUIsForType<TParent>();
        int GetMaxOrderForParent<TParent>();
        void RegisterMenu(string label, Type uiType, Type parentType = null); 
        void RegisterMenu(string label, Type menuType, int order, Type parentMenuType = null);
        void BuildMenuHierarchy();
        object Resolve(Type uiType);
    }

    public class UIRegistry : IUIRegistry
    {
        private readonly IServiceProvider provider;
        private readonly IUIFlowController flowController;

        private readonly List<UIDescriptor> descriptors = new();

        public UIRegistry(IServiceProvider provider, IUIFlowController flowController)
        {
            this.provider = provider;
            this.flowController = flowController;
        }

        public T Get<T>() where T : UI.UIBase
        {
            return provider.GetRequiredService<T>();
        }

        public IEnumerable<UIDescriptor> GetAllUIs()
        {
            return descriptors;
        }

        public IEnumerable<UIDescriptor> GetAllUIsForType<TParent>()
        {
            return descriptors
            .Where(d => d.ParentType == typeof(TParent))
            .OrderBy(d => d.Order);
        }

        public void RegisterMenu(string label, Type uiType, Type parentType = null)
        {
            var order = descriptors.Count(d => d.ParentType == parentType);
            RegisterMenu(label, uiType, order + 1, parentType);
        }

        public void RegisterMenu(string label, Type uiType, int order, Type parentMenuType = null)
        {
            if (!typeof(IDisplayableUI).IsAssignableFrom(uiType))
                throw new ArgumentException($"{uiType.Name} must implement IDisplayableUI");

            descriptors.Add(new UIDescriptor(label, uiType, order, parentMenuType));
        }

        public void BuildMenuHierarchy()
        {
            // Group by parent screen type
            foreach (var group in descriptors.GroupBy(d => d.ParentType))
            {
                if(group.Key == null)
                {
                    continue;
                }
                // Try to get parent
                var parentInstance = provider.GetRequiredService(group.Key);

                // Only menus have AddMenuAction
                if (parentInstance is not Menu parentMenu)
                    continue;

                foreach (var descriptor in group.OrderBy(d => d.Order))
                {
                    var childInstance = provider.GetRequiredService(descriptor.Type);

                    if (childInstance is IDisplayableUI ui)
                    {
                        // Allow any screen (menu or simple UI) to be registered
                        parentMenu.AddMenuAction(
                            descriptor.Label, 
                            () => flowController.NavigateTo(descriptor.Type), 
                            descriptor.Order
                            );
                    }
                    else
                    {
                        throw new InvalidOperationException(
                            $"{descriptor.Type.Name} does not implement IDisplayableUI");
                    }
                }
            }
        }

        public object Resolve(Type uiType)
        {
            return provider.GetRequiredService(uiType);
        }

        public int GetMaxOrderForParent<TParent>()
        {
            return descriptors.Count(d => d.ParentType == typeof(TParent));
        }
    }
}

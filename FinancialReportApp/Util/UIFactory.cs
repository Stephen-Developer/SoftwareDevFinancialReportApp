using FinancialReportApp.UI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Util
{
    public interface IUIFactory
    {
        T Create<T>() where T : class, IDisplayableUI;
        IDisplayableUI Create(Type uiType);
    }

    public class UIFactory : IUIFactory
    {
        private readonly IServiceProvider provider;

        public UIFactory(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public T Create<T>() where T : class, IDisplayableUI
            => provider.GetRequiredService<T>();

        public IDisplayableUI Create(Type uiType)
            => (IDisplayableUI)provider.GetRequiredService(uiType);
    }
}

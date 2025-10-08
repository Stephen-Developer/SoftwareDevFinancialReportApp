using FinancialReportApp.UI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Util
{
    public interface IUIFlowController
    {
        void NavigateTo(Type uiType);
        void NavigateBack();
        void Exit();
    }

    public class UIFlowController : IUIFlowController
    {
        private readonly IServiceProvider provider;
        private readonly Stack<IDisplayableUI> history = new();

        public UIFlowController(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public void NavigateTo(Type uiType)
        {
            var next = (IDisplayableUI)provider.GetRequiredService(uiType);
            history.Push(next);
            next.Display();
            history.Pop();
        }

        public void NavigateBack()
        {
            if (history.Count > 1)
            {
                history.Pop();
                var previous = history.Peek();
                previous.Display();
            }
        }

        public void Exit()
        {
            history.Clear();
            Environment.Exit(0);
        }
    }
}

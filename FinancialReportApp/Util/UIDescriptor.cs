using FinancialReportApp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Util
{
    public interface IMenu
    {
        void Display();
    }

    public class UIDescriptor
    {
        public string Label { get; }
        public UI.UIBase Menu { get; }
        public int Order { get; }

        public UIDescriptor(string label, UI.UIBase menu, int order = 0)
        {
            Label = label;
            Menu = menu;
            Order = order;
        }
    }
}

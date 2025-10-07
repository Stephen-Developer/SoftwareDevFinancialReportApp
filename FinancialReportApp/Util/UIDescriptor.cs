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
        public Type Type { get; }
        public int Order { get; }
        public Type ParentType { get; }

        public UIDescriptor(string label, Type type, int order = 0, Type partentType = null)
        {
            Label = label;
            Type = type;
            Order = order;
            ParentType = partentType;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Util
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MenuAttribute : Attribute
    {
        public string Label { get; }
        public Type ParentType { get; }

        public MenuAttribute(string label, Type parentType = null)
        {
            Label = label;
            ParentType = parentType;
        }
    }
}

using FinancialReportApp.Resources;
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
        public string LabelKey { get; }
        public Type ParentType { get; }

        public MenuAttribute(string labelKey, Type parentType = null)
        {
            LabelKey = labelKey;
            ParentType = parentType;
        }

        public string GetLocalizedLabel()
        {
            return Strings.ResourceManager.GetString(LabelKey)
                   ?? LabelKey;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Util
{
    internal class ResxLocaliser : ILocaliser
    {
        public string Get(string key)
        {
            return Resources.Strings.ResourceManager.GetString(key) ?? key;
        }

        public string Get(string key, params object[] args)
        {
            return string.Format(Get(key), args);
        }

        public string Get(ResourceManager resourceManager, string key, params object[] args)
        {
            return resourceManager.GetString(key) ?? key;
        }
    }
}

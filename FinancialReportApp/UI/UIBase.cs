using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    public abstract class UIBase : IDisplayableUI
    {
        protected IUserInterface userInterface;

        protected UIBase(IUserInterface userInterface)
        {
            this.userInterface = userInterface;
        }

        public abstract void Display();
    }
}

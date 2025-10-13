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
        protected ILocaliser localiser;

        protected UIBase(IUserInterface userInterface, ILocaliser localiser)
        {
            this.userInterface = userInterface;
            this.localiser = localiser;
        }

        public abstract void Display();
    }
}

using FinancialReportApp.Resources;
using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    public abstract class Menu : UIBase
    {
        protected readonly SortedDictionary<int, (string label, Action action)> menuActions = new SortedDictionary<int, (string label, Action action)>();

        protected bool exit = false;
        protected string menuBack;
        protected string menuExit;
        protected string menuStartText;
        protected string menuEndText;
        protected string menuErrorText;

        protected Menu(IUserInterface userInterface, ILocaliser localiser, string? startText, string? endText, string? errorText) : base(userInterface, localiser)
        {
            this.menuBack = localiser.Get(nameof(Strings.Menu_Message_Back));
            this.menuExit = localiser.Get(nameof(Strings.Menu_Message_Exit));

            menuStartText = startText ?? localiser.Get(nameof(Strings.Menu_Message_Start));
            menuEndText = endText ?? localiser.Get(nameof(Strings.Menu_Message_End)); ;
            menuErrorText = errorText ?? localiser.Get(nameof(Strings.Menu_Message_Error)); ;
        }

        public void AddMenuAction(string label, Action action, int? order = null)
        {
            int key = order ?? (menuActions.Count > 0 ? menuActions.Keys.Max() + 1 : 1);
            menuActions[key] = (label, action);
        }

        public override void Display()
        {
            exit = false;
            while (!exit)
            {
                userInterface.Clear();
                userInterface.WriteLine(menuStartText);
                foreach (var item in menuActions)
                {
                    userInterface.WriteLine(localiser.Get(nameof(Strings.Menu_Message_MenuOption), item.Key, item.Value.label));
                }
                userInterface.Write(menuEndText);
                if (int.TryParse(userInterface.ReadLine(), out int choice) && menuActions.ContainsKey(choice))
                {
                    userInterface.Clear();
                    menuActions[choice].action();
                }
                else
                {
                    userInterface.Clear();
                    userInterface.WriteLine(menuErrorText);
                    userInterface.WaitForKey();
                }
            }
        }

        public void Exit() => exit = true;
    }
}

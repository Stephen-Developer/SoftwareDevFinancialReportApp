using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    public abstract class Menu : UIBase
    {
        protected const string BACK = "Back";
        protected const string EXIT = "Exit";

        protected readonly SortedDictionary<int, (string label, Action action)> menuActions = new SortedDictionary<int, (string label, Action action)>();

        protected bool exit = false;
        protected string menuStartText;
        protected string menuEndText;
        protected string menuErrorText;

        protected Menu(IUserInterface userInterface, string startText, string endText, string errorText) : base(userInterface)
        {
            menuStartText = startText;
            menuEndText = endText;
            menuErrorText = errorText;
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
                    userInterface.WriteLine($"{item.Key}. {item.Value.label}");
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

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

        public void AddMenuAction(int key, string label, Action action)
        {
            menuActions[key] = (label, action);
        }

        public override void Display()
        {
            exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine(menuStartText);
                foreach (var item in menuActions)
                {
                    Console.WriteLine($"{item.Key}. {item.Value.label}");
                }
                Console.Write(menuEndText);
                if (int.TryParse(Console.ReadLine(), out int choice) && menuActions.ContainsKey(choice))
                {
                    Console.Clear();
                    menuActions[choice].action();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(menuErrorText);
                    Console.ReadKey();
                }
            }
        }

        public void Exit() => exit = true;
    }
}

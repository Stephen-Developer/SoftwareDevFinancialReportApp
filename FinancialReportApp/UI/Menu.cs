using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.UI
{
    abstract class Menu : UI
    {
        protected const string BACK = "Back";

        protected readonly Dictionary<int, (string label, Action action)> menuActions = new Dictionary<int, (string label, Action action)>();

        protected bool exit = false;
        protected string menuStartText;
        protected string menuEndText;
        protected string menuErrorText;

        protected Menu(string startText, string endText, string errorText)
        {
            menuStartText = startText;
            menuEndText = endText;
            menuErrorText = errorText;
        }

        public void AddMenuAction(string label, Action action)
        {
            int key = menuActions.Count > 0 ? menuActions.Keys.Max() + 1 : 1;
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

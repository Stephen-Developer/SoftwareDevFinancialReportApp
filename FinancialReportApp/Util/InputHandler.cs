using FinancialReportApp.Resources;
using FinancialReportApp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Util
{
    internal class InputHandler : IInputHandler
    {
        private readonly IUserInterface userInterface;
        private readonly ILocaliser localiser;

        private HashSet<string>? yesInputs;
        private HashSet<string>? noInputs;

        private readonly ResourceManager resourceManager;

        private HashSet<string> YesInputs =>
            yesInputs ??= LoadInputSet(nameof(Strings_InputHandling.PromptYesNo_YesInputs));

        private HashSet<string> NoInputs =>
            noInputs ??= LoadInputSet(nameof(Strings_InputHandling.PromptYesNo_NoInputs));

        private HashSet<string> LoadInputSet(string key) =>
            localiser.Get(resourceManager, key)
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim().ToLowerInvariant())
                .ToHashSet();

        public InputHandler(IUserInterface userInterface, ILocaliser localiser)
        {
            this.userInterface = userInterface;
            this.localiser = localiser;

            resourceManager = Strings_InputHandling.ResourceManager;
        }

        public int PromptInt(string message)
        {
            int result;
            while (true)
            {
                userInterface.Write(message);
                string input = userInterface.ReadLine();
                if (int.TryParse(input, out result))
                {
                    return result;
                }
                userInterface.WriteLine(localiser.Get(resourceManager, nameof(Strings_InputHandling.PromptInt_Invalid)));
            }
        }

        public decimal? PromptNullableDecimal(string message, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
        {
            decimal result;
            while (true)
            {
                userInterface.Write(message);
                string? input = userInterface.ReadLine();

                if(string.IsNullOrEmpty(input))
                {
                    return null;
                }

                var isParsed = decimal.TryParse(input, out result);

                if (!isParsed)
                {
                    userInterface.WriteLine(localiser.Get(resourceManager, nameof(Strings_InputHandling.PromptDecimal_Invalid)));
                    continue;
                }

                if (result < min || result > max)
                {
                    userInterface.WriteLine(localiser.Get(resourceManager, nameof(Strings_InputHandling.PromptDecimal_OutOfBounds), min, max));
                    continue;
                }

                return result;
            }
        }

        public decimal PromptDecimal(string message, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
        {
            decimal result;
            while (true)
            {
                userInterface.Write(message);
                string? input = userInterface.ReadLine();
                var isParsed = decimal.TryParse(input, out result);

                if (!isParsed)
                {
                    userInterface.WriteLine(localiser.Get(resourceManager, nameof(Strings_InputHandling.PromptDecimal_Invalid)));
                    continue;
                }

                if(result < min || result > max)
                {
                    userInterface.WriteLine(localiser.Get(resourceManager, nameof(Strings_InputHandling.PromptDecimal_OutOfBounds)));
                    continue;
                }

                return result;
            }
        }

        public string PromptString(string message)
        {
            while (true)
            {
                userInterface.Write(message);
                string input = userInterface.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                userInterface.WriteLine(localiser.Get(resourceManager, nameof(Strings_InputHandling.PromptString_Invalid)));
            }
        }

        public T PromptEnum<T>(string message) where T : Enum
        {
            while (true)
            {
                userInterface.WriteLine(message);
                var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
                for (int i = 0; i < values.Count; i++)
                {
                    userInterface.WriteLine($"{i + 1}. {values[i]}");
                }
                userInterface.Write(localiser.Get(resourceManager, nameof(Strings_InputHandling.PromptEnum_Option)));
                string input = userInterface.ReadLine();
                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= values.Count)
                {
                    return values[choice - 1];
                }
                userInterface.WriteLine(localiser.Get(resourceManager, nameof(Strings_InputHandling.PromptEnum_Invalid)));
            }
        }

        public bool PromptYesNo(string message)
        {
            while (true)
            {
                userInterface.Write(message + localiser.Get(resourceManager, nameof(Strings_InputHandling.PromptYesNo_QuestionSuffix)));
                string input = userInterface.ReadLine()?.Trim().ToLowerInvariant();
                if (YesInputs.Contains(input))
                {
                    return true;
                }
                else if (NoInputs.Contains(input))
                {
                    return false;
                }
                userInterface.WriteLine(localiser.Get(resourceManager, nameof(Strings_InputHandling.PromptYesNo_Invalid)));
            }
        }
    }
}

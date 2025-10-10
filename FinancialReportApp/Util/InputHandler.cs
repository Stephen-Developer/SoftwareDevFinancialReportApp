using FinancialReportApp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Util
{
    internal class InputHandler : IInputHandler
    {
        private readonly IUserInterface userInterface;

        public InputHandler(IUserInterface userInterface)
        {
            this.userInterface = userInterface;
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
                userInterface.WriteLine("Invalid input. Please enter a valid integer.");
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
                    userInterface.WriteLine("Invalid input. Please enter a valid decimal number.");
                    continue;
                }

                if (result < min || result > max)
                {
                    userInterface.WriteLine($"Input must be between {min} and {max}. Please enter a valid decimal number.");
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
                    userInterface.WriteLine("Invalid input. Please enter a valid decimal number.");
                    continue;
                }

                if(result < min || result > max)
                {
                    userInterface.WriteLine($"Input must be between {min} and {max}. Please enter a valid decimal number.");
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
                userInterface.WriteLine("Input cannot be empty. Please enter a valid string.");
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
                userInterface.Write("Select an option: ");
                string input = userInterface.ReadLine();
                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= values.Count)
                {
                    return values[choice - 1];
                }
                userInterface.WriteLine("Invalid input. Please select a valid option.");
            }
        }

        public bool PromptYesNo(string message)
        {
            while (true)
            {
                userInterface.Write(message + " (y/n): ");
                string input = userInterface.ReadLine().Trim().ToLower();
                if (input == "y" || input == "yes")
                {
                    return true;
                }
                else if (input == "n" || input == "no")
                {
                    return false;
                }
                userInterface.WriteLine("Invalid input. Please enter 'y' or 'n'.");
            }
        }
    }
}

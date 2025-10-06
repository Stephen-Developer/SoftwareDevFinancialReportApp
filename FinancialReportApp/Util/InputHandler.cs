using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Util
{
    internal class InputHandler
    {
        public static int PromtInt(string message)
        {
            int result;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (int.TryParse(input, out result))
                {
                    return result;
                }
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }

        public static decimal? PromtNullableDecimal(string message, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
        {
            decimal result;
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if(string.IsNullOrEmpty(input))
                {
                    return null;
                }

                var isParsed = decimal.TryParse(input, out result);

                if (!isParsed)
                {
                    Console.WriteLine("Invalid input. Please enter a valid decimal number.");
                    continue;
                }

                if (result <= min || result >= max)
                {
                    Console.WriteLine($"Input must be between {min} and {max}. Please enter a valid decimal number.");
                    continue;
                }

                return result;
            }
        }

        public static decimal PromtDecimal(string message, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
        {
            decimal result;
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();
                var isParsed = decimal.TryParse(input, out result);

                if (!isParsed)
                {
                    Console.WriteLine("Invalid input. Please enter a valid decimal number.");
                    continue;
                }

                if(result < min || result > max)
                {
                    Console.WriteLine($"Input must be between {min} and {max}. Please enter a valid decimal number.");
                    continue;
                }

                return result;
            }
        }

        public static string PromtString(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                Console.WriteLine("Input cannot be empty. Please enter a valid string.");
            }
        }

        public static T PromtEnum<T>(string message) where T : Enum
        {
            while (true)
            {
                Console.WriteLine(message);
                var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
                for (int i = 0; i < values.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {values[i]}");
                }
                Console.Write("Select an option: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= values.Count)
                {
                    return values[choice - 1];
                }
                Console.WriteLine("Invalid input. Please select a valid option.");
            }
        }

        public static bool PromtYesNo(string message)
        {
            while (true)
            {
                Console.Write(message + " (y/n): ");
                string input = Console.ReadLine().Trim().ToLower();
                if (input == "y" || input == "yes")
                {
                    return true;
                }
                else if (input == "n" || input == "no")
                {
                    return false;
                }
                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
            }
        }
    }
}

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

        public static decimal PromtDecimal(string message)
        {
            decimal result;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (decimal.TryParse(input, out result))
                {
                    return result;
                }
                Console.WriteLine("Invalid input. Please enter a valid decimal number.");
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
    }
}

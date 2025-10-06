using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Util
{
    internal class FileUtils
    {
        public static bool WriteToFile(string output, string filePath)
        {
            try
            {
                File.WriteAllText(filePath, output);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing report to file: {ex.Message}");
                return false;
            }
        }
    }
}

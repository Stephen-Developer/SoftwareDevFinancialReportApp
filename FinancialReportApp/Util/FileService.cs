using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Util
{
    internal class FileService : IFileService
    {
        public string GetAppDirectory() => AppDomain.CurrentDomain.BaseDirectory;

        public bool TryWriteFile(string filePath, string content)
        {
            try
            {
                File.WriteAllText(filePath, content);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

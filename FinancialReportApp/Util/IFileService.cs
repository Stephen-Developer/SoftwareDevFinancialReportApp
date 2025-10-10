using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Util
{
    public interface IFileService
    {
        bool TryWriteFile(string filePath, string content);
        string GetAppDirectory();
    }
}

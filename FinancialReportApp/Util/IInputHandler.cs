using System;

namespace FinancialReportApp.Util
{
    public interface IInputHandler
    {
        int PromptInt(string message);
        decimal? PromptNullableDecimal(string message, decimal min = decimal.MinValue, decimal max = decimal.MaxValue);
        decimal PromptDecimal(string message, decimal min = decimal.MinValue, decimal max = decimal.MaxValue);
        string PromptString(string message);
        T PromptEnum<T>(string message) where T : Enum;
        bool PromptYesNo(string message);
    }
}

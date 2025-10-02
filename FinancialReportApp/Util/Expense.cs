using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Util
{
    internal class Expense
    {
        private readonly string category;
        private readonly decimal amount;
        private readonly TimeFrequency frequency;

        public string Category { get => category; }
        public decimal Amount { get => amount; }
        public TimeFrequency Frequency { get => frequency; }

        public Expense(string category, decimal amount, TimeFrequency frequency)
        {
            this.category = category;
            this.amount = amount;
            this.frequency = frequency;
        }
    }
}

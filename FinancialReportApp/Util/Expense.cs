using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Util
{
    internal class Expense
    {
        private readonly string catagory;
        private readonly decimal amount;
        private readonly TimeFrequency frequency;

        public string Catagory { get => catagory; }
        public decimal Amount { get => amount; }
        public TimeFrequency Frequency { get => frequency; }

        public Expense(string catagory, decimal amount, TimeFrequency frequency)
        {
            this.catagory = catagory;
            this.amount = amount;
            this.frequency = frequency;
        }
    }
}

using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Systems
{
    public interface ITaxSystem
    {
        decimal CalculateTax(decimal income);
    }

    internal class TaxSystem : ITaxSystem
    {
        private readonly List<TaxBracket> defaultTaxBracketList = new List<TaxBracket>
        {
            new TaxBracket (0, 20, 20),
            new TaxBracket (20, null, 40)
        };

        private readonly IUserData userData;

        public TaxSystem(IUserData userData)
        {
            this.userData = userData;
        }

        public decimal CalculateTax(decimal income)
        {
            if (userData.UseCustomTax)
            {
                return 0;
            }

            decimal taxOwed = 0;

            foreach (var bracket in defaultTaxBracketList)
            {
                if (income > bracket.LowerBoundary)
                {
                    decimal incomeInBracket;
                    if (bracket.UpperBoundary.HasValue && income > bracket.UpperBoundary.Value)
                    {
                        incomeInBracket = Math.Min(income, bracket.UpperBoundary.Value) - bracket.LowerBoundary;
                    }
                    else
                    {
                        incomeInBracket = income - bracket.LowerBoundary;
                    }
                    taxOwed += incomeInBracket * (bracket.Rate / 100);
                }
                else
                {
                    break;
                }
            }

            return taxOwed;
        }
    }
}

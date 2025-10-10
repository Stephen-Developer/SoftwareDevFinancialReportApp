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

    public class TaxSystem : ITaxSystem
    {
        private readonly List<TaxBracket> defaultTaxBracketList = new List<TaxBracket>
        {
            new TaxBracket (0, 20000, 20),
            new TaxBracket (20000, null, 40)
        };

        private readonly IUserData userData;

        public TaxSystem(IUserData userData)
        {
            this.userData = userData;
        }

        public decimal CalculateTax(decimal income)
        {
            var taxBrackets = userData.UseCustomTax ? userData.CustomTaxBrackets : defaultTaxBracketList;

            decimal taxOwed = 0;

            foreach (var bracket in taxBrackets)
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

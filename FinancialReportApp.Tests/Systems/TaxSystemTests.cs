using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialReportApp.Systems;
using FinancialReportApp.Util;
using Moq;

namespace FinancialReportApp.Tests.Systems
{
    [TestClass]
    public class TaxSystemTests
    {
        [TestMethod]
        public void CalculateTaxTestDefaultTax()
        {
            var mockUserData = new Mock<IUserData>();
            mockUserData.SetupGet(u => u.UseCustomTax).Returns(false);

            var taxSystem = new TaxSystem(mockUserData.Object);
            var income = 60000m;
            
            decimal tax = taxSystem.CalculateTax(income);
            
            decimal expectedTax = (20000 * 0.20m) + (40000 * 0.40m);
            Assert.AreEqual(expectedTax, tax);
        }

        [TestMethod]
        public void CalculateTaxTestCustomTax()
        {
            var mockUserData = new Mock<IUserData>();
            var taxSystem = new TaxSystem(mockUserData.Object);

            var income = 60000m;
            var taxBrackets = new List<TaxBracket>
            {
                new TaxBracket (0, 30000, 10),
                new TaxBracket (30000, 50000, 50),
                new TaxBracket (50000, null, 90)
            };

            mockUserData.SetupGet(u => u.UseCustomTax).Returns(true);
            mockUserData.SetupGet(u => u.CustomTaxBrackets).Returns(taxBrackets);

            decimal tax = taxSystem.CalculateTax(income);
            
            decimal expectedTax = (30000 * 0.10m) + (20000 * 0.50m) + (10000 * 0.90m);
            Assert.AreEqual(expectedTax, tax);
        }
    }
}

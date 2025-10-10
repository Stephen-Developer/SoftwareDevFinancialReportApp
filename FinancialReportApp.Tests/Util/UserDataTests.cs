using FinancialReportApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Tests.Util
{
    [TestClass]
    public class UserDataTests
    {
        [TestMethod]
        public void ClearAllData_ShouldResetAllProperties()
        {
            // Arrange
            var userData = new UserData
            {
                Salary = 50000m,
                IsSalaryBeforeTax = false,
                TaxPaid = 10000m,
                SalaryFrequency = TimeFrequency.Yearly,
                UseCustomTax = true
            };

            userData.AddExpense("Rent", 1000m, TimeFrequency.Monthly);
            userData.AddTaxBracket(0m, 35000m, 0.2m);
            userData.AddTaxCredit(1650m);
            
            userData.ClearAllData();
            
            Assert.AreEqual(0m, userData.Salary);
            Assert.IsTrue(userData.IsSalaryBeforeTax);
            Assert.AreEqual(0m, userData.TaxPaid);
            Assert.AreEqual(default(TimeFrequency), userData.SalaryFrequency);
            Assert.AreEqual(0, userData.Expenses.Count);
            Assert.IsFalse(userData.UseCustomTax);
            Assert.AreEqual(0, userData.CustomTaxBrackets.Count);
            Assert.AreEqual(0, userData.TaxCredits.Count);
        }

        [TestMethod]
        public void AddAndRemoveExpense_ShouldModifyExpensesList()
        {
            var userData = new UserData();
            userData.AddExpense("Groceries", 200m, TimeFrequency.Weekly);
            userData.AddExpense("Utilities", 150m, TimeFrequency.Monthly);
            
            userData.RemoveExpense(0); 
            
            Assert.AreEqual(1, userData.Expenses.Count);
            Assert.AreEqual("Utilities", userData.Expenses[0].Category);
        }

        [TestMethod]
        public void AddAndRemoveTaxCredits_ShouldModifyCustomTaxCreditsList()
        {
            var userData = new UserData();
            userData.AddTaxCredit(500m);
            userData.AddTaxCredit(1000m);

            Assert.AreEqual(2, userData.TaxCredits.Count);

            userData.RemoveTaxCredit(0);
            
            Assert.AreEqual(1, userData.TaxCredits.Count);
            Assert.AreEqual(1000m, userData.TaxCredits[0]);
        }
    }
}

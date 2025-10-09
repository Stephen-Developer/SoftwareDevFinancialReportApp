using FinancialReportApp.Systems;
using FinancialReportApp.Util;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Tests.Systems
{
    [TestClass]
    public class ReportGeneratorTests
    {
        private Mock<IUserData> mockUserData;
        private Mock<ITaxSystem> mockTaxSystem;

        [TestInitialize]
        public void Setup()
        {
            mockUserData = new Moq.Mock<Util.IUserData>();
            mockTaxSystem = new Moq.Mock<FinancialReportApp.Systems.ITaxSystem>();
        }

        [TestMethod]
        public void TestProcessData_NoExpenses()
        {
            mockUserData.SetupGet(u => u.Salary).Returns(60000);
            mockUserData.SetupGet(u => u.IsSalaryBeforeTax).Returns(true);
            mockUserData.SetupGet(u => u.SalaryFrequency).Returns(Util.TimeFrequency.Yearly);
            mockUserData.SetupGet(u => u.Expenses).Returns(new List<Util.Expense>());
            mockUserData.SetupGet(u => u.TaxCredits).Returns(new List<decimal> { 3300 });
            mockTaxSystem.Setup(t => t.CalculateTax(60000)).Returns(13000);
            
            var reportGenerator = new ReportGenerator(mockUserData.Object, mockTaxSystem.Object);
            reportGenerator.ProcessData();
            var reportData = reportGenerator.ReportData;

            mockTaxSystem.Verify(t => t.CalculateTax(60000), Times.Once);

            Assert.AreEqual(60000, reportData.Salary.Annual);
            Assert.AreEqual(13000, reportData.Tax.Annual);
            Assert.AreEqual(3300, reportData.TaxCredits.Annual);
            Assert.AreEqual(50300, reportData.Net.Annual);
            Assert.AreEqual(0, reportData.TotalExpenses.Annual);
            Assert.AreEqual(0, reportData.Expenses.Count);
            Assert.AreEqual(5000, reportData.Salary.Monthly);
            Assert.AreEqual(1083.33m, Math.Round(reportData.Tax.Monthly, 2));
            Assert.AreEqual(275, reportData.TaxCredits.Monthly);
            Assert.AreEqual(4191.67m, Math.Round(reportData.Net.Monthly, 2));
            Assert.AreEqual(0, reportData.TotalExpenses.Monthly);
            Assert.AreEqual(1153.85m, Math.Round(reportData.Salary.Weekly, 2));
            Assert.AreEqual(250, Math.Round(reportData.Tax.Weekly, 2));
            Assert.AreEqual(63.46m, Math.Round(reportData.TaxCredits.Weekly, 2));
            Assert.AreEqual(967.31m, Math.Round(reportData.Net.Weekly, 2));
            Assert.AreEqual(0, reportData.TotalExpenses.Weekly);
        }

        [TestMethod]
        public void TestProcessData_WithExpenses()
        {
            List<Expense> expenses= new List<Expense>
            {
                new Expense("Rent", 1000, TimeFrequency.Monthly),
                new Expense("Groceries", 200, TimeFrequency.Weekly)
            };

            var totalAnnualExpenses = (1000 * 12) + (200 * 52);

            mockUserData.SetupGet(u => u.Salary).Returns(60000);
            mockUserData.SetupGet(u => u.IsSalaryBeforeTax).Returns(true);
            mockUserData.SetupGet(u => u.SalaryFrequency).Returns(Util.TimeFrequency.Yearly);
            mockUserData.SetupGet(u => u.Expenses).Returns(expenses);
            mockUserData.SetupGet(u => u.TaxCredits).Returns(new List<decimal> { 3300 });
            mockTaxSystem.Setup(t => t.CalculateTax(60000)).Returns(13000);

            var reportGenerator = new ReportGenerator(mockUserData.Object, mockTaxSystem.Object);
            reportGenerator.ProcessData();
            var reportData = reportGenerator.ReportData;

            mockTaxSystem.Verify(t => t.CalculateTax(60000), Times.Once);

            Assert.AreEqual(60000, reportData.Salary.Annual);
            Assert.AreEqual(13000, reportData.Tax.Annual);
            Assert.AreEqual(3300, reportData.TaxCredits.Annual);
            Assert.AreEqual(50300, reportData.Net.Annual);
            Assert.AreEqual(totalAnnualExpenses, reportData.TotalExpenses.Annual);
            Assert.AreEqual(2, reportData.Expenses.Count);
            Assert.AreEqual(5000, reportData.Salary.Monthly);
            Assert.AreEqual(1083.33m, Math.Round(reportData.Tax.Monthly, 2));
            Assert.AreEqual(275, reportData.TaxCredits.Monthly);
            Assert.AreEqual(4191.67m, Math.Round(reportData.Net.Monthly, 2));
            Assert.AreEqual(totalAnnualExpenses/12m, reportData.TotalExpenses.Monthly);
            Assert.AreEqual(1153.85m, Math.Round(reportData.Salary.Weekly, 2));
            Assert.AreEqual(250, Math.Round(reportData.Tax.Weekly, 2));
            Assert.AreEqual(63.46m, Math.Round(reportData.TaxCredits.Weekly, 2));
            Assert.AreEqual(967.31m, Math.Round(reportData.Net.Weekly, 2));
            Assert.AreEqual(totalAnnualExpenses/52m, reportData.TotalExpenses.Weekly);
        }

        [TestMethod]
        public void GenerateReport_ReturnsNonEmptyString()
        {
            mockUserData.SetupGet(u => u.Salary).Returns(60000);
            mockUserData.SetupGet(u => u.IsSalaryBeforeTax).Returns(true);
            mockUserData.SetupGet(u => u.SalaryFrequency).Returns(Util.TimeFrequency.Yearly);
            mockUserData.SetupGet(u => u.Expenses).Returns(new List<Util.Expense>());
            mockUserData.SetupGet(u => u.TaxCredits).Returns(new List<decimal> { 3300 });
            mockTaxSystem.Setup(t => t.CalculateTax(60000)).Returns(13000);

            var reportGenerator = new ReportGenerator(mockUserData.Object, mockTaxSystem.Object);
            reportGenerator.ProcessData();
            var report = reportGenerator.GenerateReport();

            mockTaxSystem.Verify(t => t.CalculateTax(60000), Times.Once);

            Assert.IsFalse(string.IsNullOrWhiteSpace(report));
        }

        [TestMethod]
        public void GenerateReport_ContainsExpectedValues()
        {
            mockUserData.SetupGet(u => u.Salary).Returns(60000);
            mockUserData.SetupGet(u => u.IsSalaryBeforeTax).Returns(true);
            mockUserData.SetupGet(u => u.SalaryFrequency).Returns(Util.TimeFrequency.Yearly);
            mockUserData.SetupGet(u => u.Expenses).Returns(new List<Util.Expense>());
            mockUserData.SetupGet(u => u.TaxCredits).Returns(new List<decimal> { 3300 });
            mockTaxSystem.Setup(t => t.CalculateTax(60000)).Returns(13000);
            
            var reportGenerator = new ReportGenerator(mockUserData.Object, mockTaxSystem.Object);
            reportGenerator.ProcessData();
            var report = reportGenerator.GenerateReport();

            mockTaxSystem.Verify(t => t.CalculateTax(60000), Times.Once);

            Assert.IsTrue(report.Contains("Yearly Salary before tax: £60,000.00"));
            Assert.IsTrue(report.Contains("Yearly tax: £13,000.00"));
            Assert.IsTrue(report.Contains("Yearly tax credits: £3,300.00"));
            Assert.IsTrue(report.Contains("Yearly net: £50,300.00"));
            Assert.IsTrue(report.Contains("Total Yearly expenses: £0.00"));
            Assert.IsTrue(report.Contains("Monthly Salary before tax: £5,000.00"));
            Assert.IsTrue(report.Contains("Monthly tax: £1,083.33"));
            Assert.IsTrue(report.Contains("Monthly tax credits: £275.00"));
            Assert.IsTrue(report.Contains("Monthly net: £4,191.67"));
            Assert.IsTrue(report.Contains("Total Monthly expenses: £0.00"));
            Assert.IsTrue(report.Contains("Weekly Salary before tax: £1,153.85"));
            Assert.IsTrue(report.Contains("Weekly tax: £250.00"));
            Assert.IsTrue(report.Contains("Weekly tax credits: £63.46"));
            Assert.IsTrue(report.Contains("Weekly net: £967.31"));
            Assert.IsTrue(report.Contains("Total Weekly expenses: £0.00"));

        }
    }
}

using FinancialReportApp.UI;
using FinancialReportApp.UI.UIs;
using FinancialReportApp.Util;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Tests.UI.UIs
{
    [TestClass]
    public class ViewTaxCreditUITests
    {
        ViewTaxCreditsUI ui;

        Mock<IUserInterface> mockUserInterface;
        Mock<IUserData> mockUserData;

        [TestInitialize]
        public void Setup()
        {
            mockUserInterface = new Mock<IUserInterface>();
            mockUserData = new Mock<IUserData>();

            ui = new ViewTaxCreditsUI(mockUserInterface.Object, mockUserData.Object);
        }

        [TestMethod]
        public void Display_ShowsCredits()
        {
            List<decimal> taxCredits = new List<decimal> { 1700m, 330m };

            mockUserData.Setup(ud => ud.TaxCredits).Returns(taxCredits);

            ui.Display();

            mockUserInterface.Verify(ui => ui.Clear(), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine("Total Tax Credits: £2,030.00"), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine(" - £1,700.00"), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine(" - £330.00"), Times.Once);
            mockUserInterface.Verify(ui => ui.WaitForKey(), Times.Once);
        }
    }
}

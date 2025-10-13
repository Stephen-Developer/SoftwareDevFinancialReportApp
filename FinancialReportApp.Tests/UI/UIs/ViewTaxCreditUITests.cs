using FinancialReportApp.Resources;
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
        const string totalText = "Total {0}";
        const string creditText = " - {0}";

        ViewTaxCreditsUI ui;

        Mock<IUserInterface> mockUserInterface;
        Mock<IUserData> mockUserData;
        Mock<ILocaliser> mockLocaliser;

        [TestInitialize]
        public void Setup()
        {
            mockUserInterface = new Mock<IUserInterface>();
            mockUserData = new Mock<IUserData>();
            mockLocaliser = new Mock<ILocaliser>();

            ui = new ViewTaxCreditsUI(mockUserInterface.Object, mockLocaliser.Object, mockUserData.Object);
        }

        [TestMethod]
        public void Display_ShowsCredits()
        {
            List<decimal> taxCredits = new List<decimal> { 1700m, 330m };

            mockUserData.Setup(ud => ud.TaxCredits).Returns(taxCredits);

            mockLocaliser.Setup(l => l.Get(nameof(Strings.ViewTaxCreditsUI_Message_Total), 2030m)).Returns(string.Format(totalText, 2030m));
            mockLocaliser.Setup(l => l.Get(
                nameof(Strings.ViewTaxCreditsUI_Message_Credit), It.IsAny<object[]>()))
                .Returns((string _, object[] args) => string.Format(creditText, args));


            ui.Display();

            mockUserInterface.Verify(ui => ui.Clear(), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine(string.Format(totalText, 2030m)), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine(" - 1700"), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine(" - 330"), Times.Once);
            mockUserInterface.Verify(ui => ui.WaitForKey(), Times.Once);
        }
    }
}

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
    public class ViewCustomTaxUITests
    {
        const string bracketText = "Range: {0} - {1}. Rate: {2}";

        ViewCustomTaxUI ui;

        Mock<IUserInterface> mockUserInterface;
        Mock<IUserData> mockUserData;
        Mock<ILocaliser> mockLocaliser;

        [TestInitialize]
        public void Setup()
        {
            mockUserInterface = new Mock<IUserInterface>();
            mockUserData = new Mock<IUserData>();
            mockLocaliser = new Mock<ILocaliser>();

            ui = new ViewCustomTaxUI(mockUserInterface.Object, mockLocaliser.Object, mockUserData.Object);
        }

        [TestMethod]
        public void Display_WithCustomTaxBrackets()
        {
            var customTaxBrackets = new List<TaxBracket>
            {
                new TaxBracket(0, 35000, 0.2m),
                new TaxBracket(35000, 70000, 0.4m),
                new TaxBracket(70000, null, 0.45m)
            };

            mockUserData.Setup(ud => ud.CustomTaxBrackets).Returns(customTaxBrackets);

            mockLocaliser.Setup(l => l.Get(
                nameof(Strings.ViewCustomTaxUI_Message_Bracket), It.IsAny<object[]>()))
                .Returns((string _, object[] args) => string.Format(bracketText, args));

            ui.Display();

            mockUserInterface.Verify(ui => ui.WriteLine(string.Format(bracketText, customTaxBrackets[0].LowerBoundary, customTaxBrackets[0].UpperBoundary, customTaxBrackets[0].Rate)), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine(string.Format(bracketText, customTaxBrackets[1].LowerBoundary, customTaxBrackets[1].UpperBoundary, customTaxBrackets[1].Rate)), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine(string.Format(bracketText, customTaxBrackets[2].LowerBoundary, customTaxBrackets[2].UpperBoundary, customTaxBrackets[2].Rate)), Times.Once);
            mockUserInterface.Verify(ui => ui.WaitForKey(), Times.Once);
        }
    }
}

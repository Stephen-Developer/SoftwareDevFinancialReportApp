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
        ViewCustomTaxUI ui;

        Mock<IUserInterface> mockUserInterface;
        Mock<IUserData> mockUserData;

        [TestInitialize]
        public void Setup()
        {
            mockUserInterface = new Mock<IUserInterface>();
            mockUserData = new Mock<IUserData>();

            ui = new ViewCustomTaxUI(mockUserInterface.Object, mockUserData.Object);
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

            ui.Display();

            mockUserInterface.Verify(ui => ui.WriteLine("Range: 0 - 35000. Rate: 0.2"), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine("Range: 35000 - 70000. Rate: 0.4"), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine("Range: 70000 - . Rate: 0.45"), Times.Once);
            mockUserInterface.Verify(ui => ui.WaitForKey(), Times.Once);
        }
    }
}

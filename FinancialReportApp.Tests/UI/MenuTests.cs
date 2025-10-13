using FinancialReportApp.Resources;
using FinancialReportApp.UI;
using FinancialReportApp.Util;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Tests.UI
{
    [TestClass]
    public class MenuTests
    {
        TestMenu menu;

        Mock<IUserInterface> mockUserInterface;
        Mock<ILocaliser> mockLocaliser;

        [TestInitialize]
        public void Setup()
        {
            mockUserInterface = new Mock<IUserInterface>();
            mockLocaliser = new Mock<ILocaliser>();            

            menu = new TestMenu(mockUserInterface.Object, mockLocaliser.Object, "start", "end", "error");
        }

        [TestMethod]
        public void Display_ShowMenuOptions_ExecuteSelectedOption()
        {
            bool actionExecuted = false;

            menu.AddTestAction("Action 1", () => actionExecuted = true, 1);
            menu.AddTestAction("Action 2", () => { }, 2);
            menu.AddTestAction("Exit", () => menu.Exit(), 3);

            mockUserInterface.SetupSequence(ui => ui.ReadLine())
                .Returns("1")
                .Returns("3");

            mockLocaliser.Setup(l => l.Get(
                nameof(Strings.Menu_Message_MenuOption),
                It.IsAny<object[]>()))
                .Returns((string _, object[] args) => $"{args[0]}. {args[1]}");

            menu.Display();
            
            Assert.IsTrue(actionExecuted);
            mockUserInterface.Verify(ui => ui.Clear(), Times.Exactly(4));
            mockUserInterface.Verify(ui => ui.WriteLine("start"), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.WriteLine("1. Action 1"), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.WriteLine("2. Action 2"), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.WriteLine("3. Exit"), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.Write("end"), Times.AtLeast(2));
            mockUserInterface.Verify(ui => ui.WriteLine("error"), Times.Never);
        }

        [TestMethod]
        public void Display_InvalidInput_ShowsErrorMessage()
        {
            menu.AddTestAction("Action 1", () => { }, 1);
            menu.AddTestAction("Exit", () => menu.Exit(), 2);

            mockUserInterface.SetupSequence(ui => ui.ReadLine())
                .Returns("invalid")
                .Returns("2");

            mockLocaliser.Setup(l => l.Get(
                nameof(Strings.Menu_Message_MenuOption),
                It.IsAny<object[]>()))
                .Returns((string _, object[] args) => $"{args[0]}. {args[1]}");

            menu.Display();

            mockUserInterface.Verify(ui => ui.Clear(), Times.Exactly(4));
            mockUserInterface.Verify(ui => ui.WriteLine("start"), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.WriteLine("1. Action 1"), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.WriteLine("2. Exit"), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.Write("end"), Times.AtLeast(2));
            mockUserInterface.Verify(ui => ui.WriteLine("error"), Times.Once);
            mockUserInterface.Verify(ui => ui.WaitForKey(), Times.Once);
        }
    }

    internal class TestMenu : Menu
    {
        public TestMenu(IUserInterface userInterface, ILocaliser localiser, string startText, string endText, string errorText) : base(userInterface, localiser, startText, endText, errorText)
        {
        }

        public void AddTestAction(string label, Action action, int? order = null)
        {
            AddMenuAction(label, action, order);
        }
    }
}

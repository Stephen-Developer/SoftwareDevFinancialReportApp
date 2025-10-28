using FinancialReportApp.UI;
using FinancialReportApp.UI.Menus;
using FinancialReportApp.UI.UIs;
using FinancialReportApp.Util;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Tests.Util
{
    [TestClass]
    public class UIRegistryTests
    {
        UIRegistry registry;

        Mock<IServiceProvider> mockProvider;
        Mock<IUIFlowController> mockFlowController;
        Mock<IUserInterface> mockUserInterface;
        Mock<ILocaliser> mockLocaliser;

        [TestInitialize]
        public void Setup()
        {
            mockProvider = new Mock<IServiceProvider>();
            mockFlowController = new Mock<IUIFlowController>();
            mockUserInterface = new Mock<IUserInterface>();
            mockLocaliser = new Mock<ILocaliser>();

            registry = new UIRegistry(mockProvider.Object, mockFlowController.Object);
        }

        [TestMethod]
        public void BuildMenuHierarchy_MenusRegistered_CorrectHierarchyBuilt()
        {
            var rootMenu = new TestMenu(mockUserInterface.Object, mockLocaliser.Object);
            var childMenu = new TestMenu(mockUserInterface.Object, mockLocaliser.Object);
            var dummyScreen = new TestUI(mockUserInterface.Object, mockLocaliser.Object);

            mockProvider.Setup(p => p.GetService(typeof(TestMenu))).Returns(rootMenu);
            mockProvider.Setup(p => p.GetService(typeof(TestUI))).Returns(dummyScreen);
            mockProvider.Setup(p => p.GetService(typeof(TestMenuChild))).Returns(childMenu);

            registry.RegisterMenu("Root Menu", typeof(TestMenu));                 // root
            registry.RegisterMenu("Child Menu", typeof(TestUI), typeof(TestMenu)); // child
            registry.RegisterMenu("SubMenu", typeof(TestMenuChild), typeof(TestMenu));  // child menu
            registry.BuildMenuHierarchy();

            Assert.IsTrue(rootMenu.GetActionLabels().Contains("Exit"), "Root menu should contain Exit action.");
            Assert.IsTrue(childMenu.GetActionLabels().Contains("Back"), "Child menu should contain Back action.");
            Assert.IsTrue(rootMenu.GetActionLabels().Contains("Child Menu"), "Root should have child item label added.");
        }
    }

    public class TestMenu : Menu, IDisplayableUI
    {
        public List<string> AddedActions { get; } 

        public TestMenu(IUserInterface ui, ILocaliser localiser) : base(ui, localiser, "start", "end", "error") { }

        public override void Display() { }

        public new void AddMenuAction(string label, Action action, int? order = null)
        {
            AddedActions.Add(label);
            base.AddMenuAction(label, action, order);
        }

        public IEnumerable<string> GetActionLabels() => menuActions.Values.Select(v => v.label);
    }

    public class TestMenuChild : Menu, IDisplayableUI
    {
        public TestMenuChild(IUserInterface userInterface, ILocaliser localiser, string startText, string endText, string errorText) : base(userInterface, localiser, startText, endText, errorText)
        {
        }
    }

    public class TestUI : UIBase, IDisplayableUI
    {
        public TestUI(IUserInterface ui, ILocaliser localiser) : base(ui, localiser) { }

        public override void Display()
        {

        }
    }
}

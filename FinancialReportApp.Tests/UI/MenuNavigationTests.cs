using FinancialReportApp.UI;
using FinancialReportApp.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace FinancialReportApp.Tests.UI
{
    [TestClass]
    public class MenuNavigationTests
    {
        [TestMethod]
        public void NavigateBack_DoesNotRedisplayPreviousScreen()
        {
            var services = new ServiceCollection();
            services.AddSingleton<NavigationTracker>();
            services.AddSingleton<IUIFlowController, UIFlowController>();
            services.AddTransient<ParentScreen>();
            services.AddTransient<ChildScreen>();

            using var provider = services.BuildServiceProvider();
            var flowController = provider.GetRequiredService<IUIFlowController>();

            flowController.NavigateTo(typeof(ParentScreen));

            var tracker = provider.GetRequiredService<NavigationTracker>();
            Assert.AreEqual(1, tracker.ParentDisplays, "Parent screen should only be displayed once.");
            Assert.AreEqual(1, tracker.ChildDisplays, "Child screen should be displayed exactly once.");
        }

        [TestMethod]
        public void BuildMenuHierarchy_BackAction_ExitsMenuBeforeNavigatingBack()
        {
            var mockFlowController = new Mock<IUIFlowController>();
            var services = new ServiceCollection();
            services.AddSingleton<IUserInterface, FakeUserInterface>();
            services.AddSingleton<ILocaliser, FakeLocaliser>();
            services.AddSingleton<TestParentMenu>();
            services.AddSingleton<TestChildMenu>();

            using var provider = services.BuildServiceProvider();
            var registry = new UIRegistry(provider, mockFlowController.Object);

            registry.RegisterMenu("Parent", typeof(TestParentMenu));
            registry.RegisterMenu("Child", typeof(TestChildMenu), typeof(TestParentMenu));
            registry.BuildMenuHierarchy();

            var childMenu = provider.GetRequiredService<TestChildMenu>();
            var backAction = childMenu.GetMenuAction("Back");
            Assert.IsNotNull(backAction, "Back action should be registered for child menus.");

            backAction!();

            Assert.IsTrue(childMenu.HasExited, "Back action should mark the menu for exit before navigating back.");
            mockFlowController.Verify(fc => fc.NavigateBack(), Times.Once, "Back action should navigate back once.");
        }

        private sealed class NavigationTracker
        {
            public int ParentDisplays { get; set; }
            public int ChildDisplays { get; set; }
            public bool ChildVisited { get; set; }
        }

        private sealed class ParentScreen : IDisplayableUI
        {
            private readonly IUIFlowController flowController;
            private readonly NavigationTracker tracker;

            public ParentScreen(IUIFlowController flowController, NavigationTracker tracker)
            {
                this.flowController = flowController;
                this.tracker = tracker;
            }

            public void Display()
            {
                tracker.ParentDisplays++;
                if (!tracker.ChildVisited)
                {
                    tracker.ChildVisited = true;
                    flowController.NavigateTo(typeof(ChildScreen));
                }
            }
        }

        private sealed class ChildScreen : IDisplayableUI
        {
            private readonly IUIFlowController flowController;
            private readonly NavigationTracker tracker;

            public ChildScreen(IUIFlowController flowController, NavigationTracker tracker)
            {
                this.flowController = flowController;
                this.tracker = tracker;
            }

            public void Display()
            {
                tracker.ChildDisplays++;
                flowController.NavigateBack();
            }
        }

        private sealed class FakeUserInterface : IUserInterface
        {
            public void Clear()
            {
            }

            public string? ReadLine() => null;

            public void WaitForKey()
            {
            }

            public void Write(string text)
            {
            }

            public void WriteLine(string text)
            {
            }
        }

        private sealed class FakeLocaliser : ILocaliser
        {
            public string Get(string key) => key;

            public string Get(string key, params object[] args) => string.Format(key, args);

            public string Get(ResourceManager resourceManager, string key, params object[] args) => string.Format(key, args);
        }

        private abstract class TestMenuBase : Menu
        {
            private static readonly FieldInfo MenuActionsField = typeof(Menu).GetField("menuActions", BindingFlags.NonPublic | BindingFlags.Instance)!;

            protected TestMenuBase(IUserInterface userInterface, ILocaliser localiser)
                : base(userInterface, localiser, "start", "end", "error")
            {
            }

            public bool HasExited => exit;

            public Action? GetMenuAction(string label)
            {
                if (MenuActionsField.GetValue(this) is SortedDictionary<int, (string label, Action action)> actions)
                {
                    return actions.Values.FirstOrDefault(a => string.Equals(a.label, label, StringComparison.OrdinalIgnoreCase)).action;
                }

                return null;
            }
        }

        private sealed class TestParentMenu : TestMenuBase
        {
            public TestParentMenu(IUserInterface userInterface, ILocaliser localiser)
                : base(userInterface, localiser)
            {
            }
        }

        private sealed class TestChildMenu : TestMenuBase
        {
            public TestChildMenu(IUserInterface userInterface, ILocaliser localiser)
                : base(userInterface, localiser)
            {
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace XFApp.UITests
{
    [TestFixture(Platform.Android)]
    // [TestFixture(Platform.iOS)]
    public class Tests
    {
        private IApp _app;
        private Platform _platform;

        public Tests(Platform platform)
        {
            _platform = platform;
        }

        [SetUp]
        public void Setup()
        {
            _app = AppInitializer.StartApp(_platform);
        }

        [Test]
        public void AppLaunches()
        {
            _app.Screenshot("First screen.");
        }

        [Test]
        public void RunRepl()
        {
            _app.Repl();
        }

        [Test]
        [TestCase("rhaly")]
        public void SearchForUser(string userName)
        {
            _app.WaitForElement(c => c.Id("search_src_text"));
            _app.EnterText(c => c.Marked("search_src_text"), userName);
            _app.Screenshot("Entered user");
            _app.DismissKeyboard();

            _app.WaitForElement(c => c.Text(userName).Class("TextView"), timeout: TimeSpan.FromSeconds(5));
            _app.Screenshot("Search user results");

        }

        [Test]
        [TestCase("rhaly")]
        public void NavigateToUserRepositories(string userName)
        {
            _app.WaitForElement(c => c.Id("search_src_text"));
            _app.EnterText(c => c.Marked("search_src_text"), userName);
            _app.Screenshot("Entered user");
            _app.DismissKeyboard();

            _app.WaitForElement(c => c.Text(userName).Class("TextView"), timeout: TimeSpan.FromSeconds(5));
            _app.Screenshot("Search user results");
            _app.Tap(c => c.Text(userName).Class("TextView"));
            
            _app.WaitForElement(c => c.Text("Repositories"));
            _app.Screenshot("Navigated to Repositories");
        }
    }
}

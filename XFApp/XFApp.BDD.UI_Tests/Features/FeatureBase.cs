using NUnit.Framework;
using TechTalk.SpecFlow;
using Xamarin.UITest;

namespace XFApp.BDD.UI_Tests.Features
{
    [TestFixture(Platform.Android)]
    public class FeatureBase
    {
        protected static IApp App;
        protected Platform Platform { get; }

        public FeatureBase(Platform platform)
        {
            Platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            App = AppInitializer.StartApp(Platform);
            FeatureContext.Current.Add("App", App);
            AppInitializer.InitializeScreens(Platform);
        }

        [TearDown]
        public void TearDown()
        {
            FeatureContext.Current.Remove("App");
            AppInitializer.ClearScreens(Platform);
        }
    }
}
using TechTalk.SpecFlow;
using Xamarin.UITest;
using XFApp.BDD.UI_Tests.Screens;

namespace XFApp.BDD.UI_Tests.Steps
{
    public class StepsBase
    {
        protected readonly IUserSearchPage UserSearchPage;
        protected readonly IApp App;
        protected readonly IRepositoriesPage RepositoriesPage;

        public StepsBase()
        {
            App = FeatureContext.Current.Get<IApp>("App");
            UserSearchPage = FeatureContext.Current.Get<IUserSearchPage>(ScreenNames.UserSearchPage);
            RepositoriesPage = FeatureContext.Current.Get<IRepositoriesPage>(ScreenNames.RepositoriesPage);

        }
    }
}
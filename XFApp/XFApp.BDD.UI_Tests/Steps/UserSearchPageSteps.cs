using System;
using TechTalk.SpecFlow;

namespace XFApp.BDD.UI_Tests.Steps
{
    [Binding]
    public class UserSearchPageSteps : StepsBase
    {
        [When(@"I have entered ""(.*)"" to search view")]
        public void WhenIHaveEnteredToSearchView(string searchText)
        {
            App.WaitForElement(c => c.Id(UserSearchPage.SearchInput));
            App.EnterText(c => c.Marked(UserSearchPage.SearchInput), searchText);
            App.Screenshot("Entered search text");
            App.DismissKeyboard();
        }

        [Then(@"I should see ""(.*)"" in results list")]
        [When(@"I see ""(.*)"" in results list")]
        public void ThenIShouldSeeInResultsList(string result)
        {
            App.WaitForElement(UserSearchPage.UserResult(result), timeout: TimeSpan.FromSeconds(15));
            App.Screenshot("Search user results");
        }

        [When(@"I tap ""(.*)""")]
        public void WhenITap(string result)
        {
            App.Tap(UserSearchPage.UserResult(result));
        }

        [Then(@"I should be navigated to repository page")]
        public void ThenIShouldBeNavigatedToPage()
        {
            App.WaitForElement(c => c.Text(RepositoriesPage.RepositoriesTitle));
        }
    }
}
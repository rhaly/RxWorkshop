using TechTalk.SpecFlow;

namespace XFApp.BDD.UI_Tests.Steps
{
    [Binding]
    public class CommonSteps: StepsBase
    {
        [Given(@"I am on the ""(.*)"" page")]
        [Then(@"I should see ""(.*)"" page")]
        public void GivenIAmOnThePage(string page)
        {
            App.Screenshot($"I am on the {page} page");
        }        

    }
}
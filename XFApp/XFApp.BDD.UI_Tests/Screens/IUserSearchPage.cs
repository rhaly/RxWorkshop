using System;
using Xamarin.UITest.Queries;

namespace XFApp.BDD.UI_Tests.Screens
{
    public interface IUserSearchPage
    {
        string SearchInput { get; }

        Func<AppQuery, AppQuery> UserResult(string searchQuery);
    }

    public class AndroidUserSearchPage : IUserSearchPage
    {
        public string SearchInput => "search_src_text";
        public Func<AppQuery, AppQuery> UserResult(string searchQuery)
        {
            return c => c.Text(searchQuery).Class("TextView");
        }
    }
}
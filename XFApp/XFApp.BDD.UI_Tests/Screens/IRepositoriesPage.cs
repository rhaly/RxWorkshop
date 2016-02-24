namespace XFApp.BDD.UI_Tests.Screens
{
    public interface IRepositoriesPage
    {
         string RepositoriesTitle { get; }
    }

    public class AndroidRepositoriesPage : IRepositoriesPage
    {
        public string RepositoriesTitle => "Repositories";
    }
}
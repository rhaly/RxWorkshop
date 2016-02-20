using System.ComponentModel;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace XFApp.Common.ViewModels
{
    public interface ISearchUserPageViewModel : INotifyPropertyChanged
    {
        string SearchUserText { get; set; }

        ICommand SearchUserCommand { get;}

        ICommand NavigateToUser { get;  }

        ICommand RefreshResultCommand { get; }
    }

    public class SearchUserPageViewModel : BindableBase, ISearchUserPageViewModel
    {
        public SearchUserPageViewModel()
        {
            SearchUserCommand = new DelegateCommand(SearchUser);
        }

        private string _searchUserText;

        public string SearchUserText
        {
            get { return _searchUserText; }
            set
            {
                _searchUserText = value;
                OnPropertyChanged(() => SearchUserText);
            }
        }

        public ICommand SearchUserCommand { get; set; }
        public ICommand NavigateToUser { get; set; }
        public ICommand RefreshResultCommand { get; set; }

        private void SearchUser()
        {
            //TODO delegate to user
        }
    }
}
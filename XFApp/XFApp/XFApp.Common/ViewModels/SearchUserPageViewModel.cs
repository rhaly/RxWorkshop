using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using GHApp.Contracts.Dto;
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

        ObservableCollection<User> Results { get; set; }
    }

    public class SearchUserPageViewModel : BindableBase, ISearchUserPageViewModel
    {
        public SearchUserPageViewModel()
        {
            SearchUserCommand = new DelegateCommand(SearchUser);
            RefreshResultCommand = new DelegateCommand(RefreshResults);
        }        

        private string _searchUserText;

        public string SearchUserText
        {
            get { return _searchUserText; }
            set
            {
                _searchUserText = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchUserCommand { get; set; }

        public ICommand NavigateToUser { get; set; }

        public ICommand RefreshResultCommand { get; set; }

        private ObservableCollection<User> _results;

        public ObservableCollection<User> Results
        {
            get { return _results; }
            set
            {
                _results = value;
                OnPropertyChanged();
            }
        }

        private void SearchUser()
        {
            //TODO delegate to user
        }

        private void RefreshResults()
        {
          //TODO refresh results
        }
    }
}
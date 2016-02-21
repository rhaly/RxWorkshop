using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;
using GHApp.Contracts.Dto;
using Prism.Commands;
using Prism.Mvvm;
using Refit;
using XFApp.Common.Model.Services;

namespace XFApp.Common.ViewModels
{
    public interface ISearchUserPageViewModel : INotifyPropertyChanged
    {
        string SearchUserText { get; set; }

        ICommand SearchUserCommand { get;}

        ICommand NavigateToUserCommand { get;  }


        ObservableCollection<User> Results { get; set; }
    }

    public class SearchUserPageViewModel : BindableBase, ISearchUserPageViewModel
    {
        private readonly IGitHubService _gitHubService;

        public SearchUserPageViewModel(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
            SearchUserCommand = new DelegateCommand(SearchUser);
            NavigateToUserCommand = new DelegateCommand<User>(NavigateToUserDetails);
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

        public ICommand NavigateToUserCommand { get; set; }

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
            _gitHubService.SearchUser(SearchUserText)
                
                .Subscribe(res => Results = new ObservableCollection<User>(res));
        }

        private void NavigateToUserDetails(User user)
        {
           
        }

    }
}
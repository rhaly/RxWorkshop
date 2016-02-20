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
            var gitHubApi = RestService.For<IGitHubApi>("https://api.github.com");
            

            gitHubApi.SearchUser(SearchUserText, "Basic " + Convert.ToBase64String(byteArray))
                .Subscribe(res =>
                {
                    Debug.WriteLine(res);
                }, e => OnError(e));
        }

        private void OnError(Exception exception)
        {
            Debug.WriteLine(exception);
        }

        private void RefreshResults()
        {
          //TODO refresh results
        }
    }
}
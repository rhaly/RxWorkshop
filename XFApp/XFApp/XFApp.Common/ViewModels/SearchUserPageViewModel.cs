﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Input;
using GHApp.Contracts.Dto;
using Prism.Commands;
using Prism.Navigation;
using XFApp.Common.Model;
using XFApp.Common.Model.Services;

namespace XFApp.Common.ViewModels
{
    public interface ISearchUserPageViewModel : IReactiveObject
    {
        string SearchUserText { get; set; }

        ICommand SearchUserCommand { get; }

        ICommand NavigateToUserCommand { get; }

        ObservableCollection<User> Results { get; set; }

        bool IsLoading { get; set; }
    }

    public class SearchUserPageViewModel : ReactiveObject, ISearchUserPageViewModel, INavigationAware
    {
        private readonly IGitHubService _gitHubService;
        private readonly INavigationService _navigationService;

        private readonly ISubject<bool> _isLoadingSubject = new BehaviorSubject<bool>(false);
        private readonly ISubject<string> _searchUserSubject = new Subject<string>(); 

        public SearchUserPageViewModel(IGitHubService gitHubService, INavigationService navigationService, IScheduleProvider scheduleProvider)
        {
            _gitHubService = gitHubService;
            _navigationService = navigationService;
            var scheduleProvider1 = scheduleProvider;
            SearchUserCommand = new DelegateCommand(SearchUser);
            NavigateToUserCommand = new DelegateCommand<User>(NavigateToUserDetails);

            PropertyChangedStream
                .Where(x => x == "SearchUserText")
                .Subscribe(_ => _searchUserSubject.OnNext(SearchUserText));

            _searchUserSubject
                .Throttle(TimeSpan.FromMilliseconds(500))
                .DistinctUntilChanged()
                .Where(x=>!string.IsNullOrEmpty(x))
                .Select(searchText =>
                {                    
                    _isLoadingSubject.OnNext(true);
                    return _gitHubService.SearchUser(SearchUserText);
                })
                .Switch()
                .ObserveOn(scheduleProvider1.UiScheduler)
                .Subscribe(OnUserResults, OnError);

            _isLoadingSubject
                .ObserveOn(scheduleProvider1.UiScheduler)
                .Subscribe(loading => IsLoading = loading);

        }

        private void OnError(Exception e)
        {
            _isLoadingSubject.OnNext(false);
            Debug.WriteLine(e);
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

        private ICommand _navigateToUserCommand;

        public ICommand NavigateToUserCommand
        {
            get { return _navigateToUserCommand; }
            set
            {
                _navigateToUserCommand = value;
                OnPropertyChanged();
            }
        }

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

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private void SearchUser()
        {           
            _searchUserSubject.OnNext(SearchUserText);
        }

        private void NavigateToUserDetails(User user)
        {
            var parameters = new NavigationParameters();
            parameters.Add("user",user);
            _navigationService.Navigate<UserRepositoriesPageViewModel>(parameters);
        }

        private void OnUserResults(IEnumerable<User> searchResults)
        {
            _isLoadingSubject.OnNext(false); 
            Results = new ObservableCollection<User>(searchResults);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }
    }
}
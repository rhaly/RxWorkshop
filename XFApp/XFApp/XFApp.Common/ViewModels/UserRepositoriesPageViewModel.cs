using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows.Input;
using GHApp.Contracts.Dto;
using Prism.Commands;
using Prism.Navigation;
using XFApp.Common.Model;
using XFApp.Common.Model.Services;

namespace XFApp.Common.ViewModels
{
    public interface IUserRepositoriesPageViewModel : IReactiveObject
    {
        User User { get; }

        ObservableCollection<IRepoModel> Results { get; set; }

        ICommand NavigateToCommitsCommand { get; }

        ICommand WatchRepoCommand { get; }

        ICommand RefreshCommand { get; }

        bool IsLoading { get; set; }
    }

    public class UserRepositoriesPageViewModel : ReactiveObject, IUserRepositoriesPageViewModel, INavigationAware
    {
        private readonly IRepoService _repoService;
        private readonly INavigationService _navigationService;
        private readonly IScheduleProvider _scheduleProvider;

        public UserRepositoriesPageViewModel(IRepoService repoService, INavigationService navigationService, IScheduleProvider scheduleProvider)
        {
            _repoService = repoService;
            _navigationService = navigationService;
            _scheduleProvider = scheduleProvider;
            NavigateToCommitsCommand = new DelegateCommand<IRepoModel>(NavigateToCommits);
            WatchRepoCommand = new DelegateCommand<IRepoModel>(ToggleWatchRepo);
            RefreshCommand = new DelegateCommand(LoadRepos);

        }        

        private void ToggleWatchRepo(IRepoModel repo)
        {
            IsLoading = true;
            _repoService.ToggleRepoWatch(repo)
                .SubscribeOn(_scheduleProvider.TaskPool)
                .ObserveOn(_scheduleProvider.UiScheduler)
                .Subscribe(_ => IsLoading = false);
        }

        private void NavigateToCommits(IRepoModel repo)
        {
            var parameters = new NavigationParameters();
            parameters.Add("repo", repo);
            parameters.Add("user", User);
            _navigationService.Navigate<CommitsPageViewModel>(parameters);
        }

        public User User { get; private set; }

        private ObservableCollection<IRepoModel> _results;

        public ObservableCollection<IRepoModel> Results
        {
            get { return _results; }
            set
            {
                _results = value;
                OnPropertyChanged();
            }
        }

        public ICommand NavigateToCommitsCommand { get; }

        public ICommand WatchRepoCommand { get; }

        public ICommand RefreshCommand { get; }

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

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            var user = parameters["user"] as User;
            if (user == null)
            {
                return;
            }

            User = user;
            LoadRepos();
        }

        private void LoadRepos()
        {
            IsLoading = true;
            _repoService.GetReposForUser(User.Login)
                .ObserveOn(_scheduleProvider.UiScheduler)
                .SubscribeOn(_scheduleProvider.TaskPool)
                .Subscribe(OnReposLoaded);
        }

        private void OnReposLoaded(IEnumerable<IRepoModel> res)
        {
            Results = new ObservableCollection<IRepoModel>(res);
            IsLoading = false;
        }
    }
}
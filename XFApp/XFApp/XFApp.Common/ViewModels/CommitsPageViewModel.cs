using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using GHApp.Contracts.Dto;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;
using XFApp.Common.Model;
using XFApp.Common.Model.Services;

namespace XFApp.Common.ViewModels
{
    public interface ICommitsPageViewModel : IReactiveObject
    {
        ObservableCollection<Commit> Commits { get; set; }

        IRepoModel Repo { get; }

        User User { get; }

        bool IsLoading { get; set; }

        ICommand RefreshCommand { get; }
    }


    public class CommitsPageViewModel : ReactiveObject, ICommitsPageViewModel, INavigationAware
    {
        private readonly IGitHubService _gitHubService;
        private readonly IRepoNotificationService _repoNotificationService;
        private readonly IScheduleProvider _scheduleProvider;
        private IDisposable _repoNotificationsSubscription;

        public CommitsPageViewModel(IGitHubService gitHubService, IRepoNotificationService repoNotificationService, IScheduleProvider scheduleProvider)
        {
            _gitHubService = gitHubService;
            _repoNotificationService = repoNotificationService;
            _scheduleProvider = scheduleProvider;
            RefreshCommand = new DelegateCommand(LoadCommits);
        }

        private ObservableCollection<Commit> _commits;

        public ObservableCollection<Commit> Commits
        {
            get { return _commits; }
            set
            {
                _commits = value;
                OnPropertyChanged();
            }
        }

        public IRepoModel Repo { get; private set; }

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

        public ICommand RefreshCommand { get; }

        public User User { get; private set; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            if (_repoNotificationsSubscription != null)
            {
                _repoNotificationsSubscription.Dispose();
                _repoNotificationsSubscription = null;
            }
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

            var repoModel = parameters["repo"] as IRepoModel;
            if (repoModel == null)
            {
                return;
            }
            Repo = repoModel;

            var user = parameters["user"] as User;
            if (user == null)
            {
                return;
            }

            User = user;
            LoadCommits();

            _repoNotificationsSubscription =_repoNotificationService.RepoNotificationStream
                .Where(repoUrl => repoUrl == Repo.Dto.Url.ToString())
                .Subscribe(_ => LoadCommits());

        }

        public void LoadCommits()
        {
            IsLoading = true;
            _gitHubService.GetCommits(User.Login, Repo.Dto.Name)
                .SubscribeOn(_scheduleProvider.TaskPool)
                .ObserveOn(_scheduleProvider.UiScheduler)
                .Subscribe(OnCommitsLoaded);
        }


        private void OnCommitsLoaded(IEnumerable<Commit> res)
        {
            IsLoading = false;
            Commits = new ObservableCollection<Commit>(res);
        }
    }
}
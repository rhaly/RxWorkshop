using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
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
        User User { get; set; }

        ObservableCollection<IRepoModel> Results { get; set; }

        ICommand NavigateToCommitsCommand { get; }

        ICommand WatchRepoCommand { get; }
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
        }

        private void ToggleWatchRepo(IRepoModel repo)
        {
            
        }

        private void NavigateToCommits(IRepoModel repo)
        {
           
        }

        private User _user;

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

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

            _repoService.GetReposForUser(User.Login)
                .ObserveOn(_scheduleProvider.UiScheduler)
                .SubscribeOn(_scheduleProvider.TaskPool)
                .Subscribe(res => Results = new ObservableCollection<IRepoModel>(res));
        }
    }
}
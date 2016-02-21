using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using GHApp.Contracts.Dto;
using Prism.Navigation;
using XFApp.Common.Model;
using XFApp.Common.Model.Services;

namespace XFApp.Common.ViewModels
{
    public interface IUserRepositoriesPageViewModel : IReactiveObject
    {
        User User { get; set; }

        ObservableCollection<Repo> Results { get; set; }
    }

    public class UserRepositoriesPageViewModel : ReactiveObject, IUserRepositoriesPageViewModel, INavigationAware
    {
        private readonly IGitHubService _gitHubService;
        private readonly INavigationService _navigationService;
        private readonly IScheduleProvider _scheduleProvider;

        public UserRepositoriesPageViewModel(IGitHubService gitHubService, INavigationService navigationService, IScheduleProvider scheduleProvider)
        {
            _gitHubService = gitHubService;
            _navigationService = navigationService;
            _scheduleProvider = scheduleProvider;
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

        private ObservableCollection<Repo> _results;

        public ObservableCollection<Repo> Results
        {
            get { return _results; }
            set
            {
                _results = value;
                OnPropertyChanged();
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            var user = parameters["user"] as User;
            if (user != null)
            {
                User = user;
            }

            _gitHubService.GetUserRepositories(User.Login)
                .ObserveOn(_scheduleProvider.UiScheduler)
                .SubscribeOn(_scheduleProvider.TaskPool)
                .Subscribe(res => Results = new ObservableCollection<Repo>(res));
        }
    }
}
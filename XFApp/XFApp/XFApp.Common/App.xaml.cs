using System;
using System.Diagnostics;
using GHApp.Contracts.Dto;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Refit;
using Xamarin.Forms;
using XFApp.Common.Model;
using XFApp.Common.Model.Services;
using XFApp.Common.ViewModels;
using XFApp.Common.Views;

namespace XFApp.Common
{
    public partial class App : PrismApplication
    {       
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.Navigate("MainNavigationPage/SearchUserPage", useModalNavigation: false);

           
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MainNavigationPage>();
            Container.RegisterTypeForNavigation<SearchUserPage>();
            Container.RegisterTypeForNavigation<UserRepositoriesPage,UserRepositoriesPageViewModel>();
            Container.RegisterTypeForNavigation<CommitsPage,CommitsPageViewModel>();
            
            Container.RegisterType<IAuthenticationFactory, AuthenticationFactory>();
            Container.RegisterType<IGitHubService, GitHubService>();
            Container.RegisterAsSingleton<IGitHubApi>(() => RestService.For<IGitHubApi>("https://api.github.com"));

            Container.RegisterAsSingleton<IScheduleProvider>(new ScheduleProvider());
            Container.RegisterAsSingleton<IRepoService, RepoService>();

            var repoNotificationService = new RepoNotificationService();
            Container.RegisterAsSingleton<IRepoNotificationService>(repoNotificationService);
            Container.RegisterAsSingleton<IRepoNotificationController>(repoNotificationService);

            var signalRService = new SignalRClientService(Container.Resolve<IRepoNotificationController>(),"http://192.168.0.181:22500/");
            Container.RegisterAsSingleton<ISignalRClientService>(signalRService);
        }
    }
}

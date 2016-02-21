﻿using System;
using Microsoft.Practices.Unity;
using Prism.Unity;
using Refit;
using XFApp.Common.Model.Services;
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
            Container.RegisterType<IAuthenticationFactory, AuthenticationFactory>();
            Container.RegisterType<IGitHubService, GitHubService>();
            Container.RegisterAsSingleton<IGitHubApi>(() => RestService.For<IGitHubApi>("https://api.github.com"));
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using GHApp.Contracts.Dto;

namespace XFApp.Common.Model.Services
{
    public interface IRepoService
    {
        IObservable<IEnumerable<IRepoModel>> GetReposForUser(string userName);

        IObservable<Unit> ToggleRepoWatch(IRepoModel repo);
    }

    class RepoService : IRepoService
    {
        private readonly HashSet<string> _watchedRepos = new HashSet<string>();

        private readonly IGitHubService _gitHubService;
        private readonly ISignalRClientService _signalRClientService;

        public RepoService(IGitHubService gitHubService, ISignalRClientService signalRClientService)
        {
            _gitHubService = gitHubService;
            _signalRClientService = signalRClientService;
        }


        public IObservable<IEnumerable<IRepoModel>> GetReposForUser(string userName)
        {
            return _gitHubService.GetUserRepositories(userName)
                .Select(MarkWatchedStatus);

        }

        public IObservable<Unit> ToggleRepoWatch(IRepoModel repo)
        {
            var url = repo.Dto.Url.ToString();
            if (_watchedRepos.Contains(url))
            {
                _watchedRepos.Remove(url);
                _signalRClientService.UnWatchRepo(repo.Dto);
                repo.IsWatched = false;
            }
            else
            {
                _watchedRepos.Add(url);
                _signalRClientService.WatchRepo(repo.Dto);
                repo.IsWatched = true;
            }

            return Observable.Create<Unit>(obs =>
            {
                //TODO stop or start watching call service
                obs.OnNext(Unit.Default);
                obs.OnCompleted();

                return Disposable.Empty;
            });
            
        }

        private IEnumerable<IRepoModel> MarkWatchedStatus(IEnumerable<Repo> repos)
        {
            return repos.Select(repo => new RepoModel(repo)
            {
                IsWatched = _watchedRepos.Contains(repo.Url.ToString())
            }).ToList();
        }
    }
}
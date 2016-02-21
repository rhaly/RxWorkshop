using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
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

        public RepoService(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }


        public IObservable<IEnumerable<IRepoModel>> GetReposForUser(string userName)
        {
            return _gitHubService.GetUserRepositories(userName)
                .Select(MarkWatchedStatus);

        }

        public IObservable<Unit> ToggleRepoWatch(IRepoModel repo)
        {
            throw new NotImplementedException();
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
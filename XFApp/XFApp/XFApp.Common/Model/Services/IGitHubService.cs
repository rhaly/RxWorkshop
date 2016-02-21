using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using GHApp.Contracts.Dto;

namespace XFApp.Common.Model.Services
{
    public interface IGitHubService
    {
        IObservable<IEnumerable<User>> SearchUser(string userName);

        IObservable<IEnumerable<Repo>> GetUserRepositories(string user);

        IObservable<IEnumerable<Commit>> GetCommits(string user, string repo);
    }

    class GitHubService : IGitHubService
    {
        private readonly IAuthenticationFactory _authenticationFactory;
        private readonly IGitHubApi _api;

        public GitHubService(IAuthenticationFactory authenticationFactory, IGitHubApi api)
        {
            _authenticationFactory = authenticationFactory;
            _api = api;
        }

        public IObservable<IEnumerable<User>> SearchUser(string userName)
        {
            return _api.SearchUser(userName, _authenticationFactory.BasicAuthenticationCredentials())
                .Select(x => x.Users);
        }

        public IObservable<IEnumerable<Repo>> GetUserRepositories(string user)
        {
            return _api.GetUserRepos(user, _authenticationFactory.BasicAuthenticationCredentials());
        }

        public IObservable<IEnumerable<Commit>> GetCommits(string user, string repo)
        {
            return _api.GetCommits(user, repo, _authenticationFactory.BasicAuthenticationCredentials());
        }
    }
}
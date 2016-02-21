using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using GHApp.Contracts.Dto;
using Refit;

namespace XFApp.Common.Model.Services
{
    public interface IGitHubService
    {
        IObservable<IEnumerable<User>> SearchUser(string userName);
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
    }
}
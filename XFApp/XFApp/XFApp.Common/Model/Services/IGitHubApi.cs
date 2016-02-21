using System;
using System.Collections.Generic;
using GHApp.Contracts.Dto;
using Refit;

namespace XFApp.Common.Model.Services
{
    [Headers("User-Agent: XFApp")]
    public interface IGitHubApi
    {
        [Get("/search/users?q={userNamePart}&page=1&per_page=50")]        
        IObservable<SearchResult> SearchUser(string userNamePart, [Header("Authorization")]string authorization);

        [Get("/users/{user}/repos")]
        IObservable<IEnumerable<Repo>> GetUserRepos(string user, [Header("Authorization")]string authorization);
    }
}
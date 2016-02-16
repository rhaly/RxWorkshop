using System;
using System.Collections.Generic;
using GHApp.Contracts.Dto;

namespace GHApp.Service
{
    public interface IGitHubUserSearchService
    {
        IObservable<IEnumerable<User>> FindUser(string userNamePart);
    }
}
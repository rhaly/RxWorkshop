using System;
using System.Reactive.Linq;
using GHApp.Communication;
using GHApp.Contracts.Dto;
using GHApp.Contracts.Notifications;
using GHApp.Contracts.Queries;
using GHApp.Contracts.Responses;
using Microsoft.AspNet.SignalR;
using XFApp.Backend.Hubs;

namespace XFApp.Backend.Services
{
    public interface IRepoWatch
    {
        void WatchRepo(Repo repo);

        void UnWatchRepo(Repo repo);
    }

    public class RepoWatch : IRepoWatch
    {
        private readonly IService<FavQuery, FavResponse> _favService;
        private readonly ITopic<RepoNotification> _repoTopic;
        //ITopic<RepoNotification> _repoNotifications = new Topic<RepoNotification>();

        public RepoWatch(IService<FavQuery, FavResponse> favService,ITopic<RepoNotification> repoTopic)
        {
            _favService = favService;
            _repoTopic = repoTopic;

            _repoTopic.Messages
               .Select(n => n.Commit)               
               .Subscribe(OnNewCommit);
        }

        private void OnNewCommit(Commit commit)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<RepoHub>();

            var uri = new Uri(commit.Url);

            var noLastSegment = string.Format("{0}://{1}", uri.Scheme, uri.Authority);

            for (int i = 0; i < uri.Segments.Length - 1; i++)
            {
                noLastSegment += uri.Segments[i];
            }

            noLastSegment = noLastSegment.Trim("/".ToCharArray());
            hubContext.Clients.Group(noLastSegment).RepoChanged(noLastSegment);
        }

        public void WatchRepo(Repo repo)
        {
            _favService.Query(new FavQuery(repo))
                .Subscribe();
        }

        public void UnWatchRepo(Repo repo)
        {
            
        }
    }
}
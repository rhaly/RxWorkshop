using System.Threading.Tasks;
using GHApp.Contracts.Dto;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using XFApp.Backend.Services;

namespace XFApp.Backend.Hubs
{
    [HubName("RepoHub")]
    public class RepoHub : Hub
    {
        private readonly IRepoWatch _repoWatch;

        public RepoHub(IRepoWatch repoWatch)
        {
            _repoWatch = repoWatch;
        }

        public async Task WatchRepo(Repo repo)
        {
            _repoWatch.WatchRepo(repo);
            await Groups.Add(Context.ConnectionId, repo.CommitsUrl.ToString().Replace("{/sha}", string.Empty));            
        }

        public async Task UnWatchRepo(Repo repo)
        {
            await Groups.Remove(Context.ConnectionId, repo.CommitsUrl.ToString().Replace("{/sha}", string.Empty));
        }       
    }
}
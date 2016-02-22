using System.Diagnostics;
using System.Threading.Tasks;
using GHApp.Contracts.Dto;
using Microsoft.AspNet.SignalR.Client;

namespace XFApp.Common.Model.Services
{
    public class SignalRClient : ReactiveObject
    {
        public SignalRClient(IRepoNotificationController repoNotificationController, string url)
        {
            Connection = new HubConnection(url);

            Connection.StateChanged += (StateChange obj) => {
                OnPropertyChanged(()=> ConnectionState);
            };

            RepoHubProxy = Connection.CreateHubProxy("RepoHub");
            RepoHubProxy.On<string>("RepoChanged", repo => {
                Debug.WriteLine(repo +" changed");
                repoNotificationController.NotifyRepoChanges(repo);
            });
        }

        public IHubProxy RepoHubProxy { get; set; }

        private HubConnection _connection;

        public HubConnection Connection
        {
            get { return _connection; }
            set
            {
                _connection = value; 
                OnPropertyChanged();
            }
        }

        public ConnectionState ConnectionState => Connection.State;

        public Task Start()
        {
            return Connection.Start();
        }

        public bool IsConnectedOrConnecting => Connection.State != ConnectionState.Disconnected;

        public void WatchRepo(Repo repo)
        {
            RepoHubProxy.Invoke("WatchRepo", repo);
        }

        public void UnWatchRepo(Repo repo)
        {
            RepoHubProxy.Invoke("UnWatchRepo", repo);
        }

        //public static async Task<SignalRClient> CreateAndStart(string url)
        //{
        //    var client = new SignalRClient(url);
        //    await client.Start();
        //    return client;
        //}
    }
}
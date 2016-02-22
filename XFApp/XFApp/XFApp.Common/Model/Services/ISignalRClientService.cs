using System;
using System.Diagnostics;
using System.Reactive.Linq;
using GHApp.Contracts.Dto;

namespace XFApp.Common.Model.Services
{
    public interface ISignalRClientService
    {
        void WatchRepo(Repo repo);

        void UnWatchRepo(Repo repo);

        IObservable<string> RepoChangeStream { get; }
    }

    public class SignalRClientService : ISignalRClientService
    {
        private readonly SignalRClient _client;

        public SignalRClientService(string http)
        {
            _client = new SignalRClient(http);
            _client.Start().ContinueWith(task => {
                if (task.IsFaulted)
                    Debug.WriteLine("An error occurred when trying to connect to SignalR: " + task.Exception.InnerExceptions[0].Message, "OK");

            });

            //TODO dispose // subscribe to connection changes
            Observable.Interval(TimeSpan.FromSeconds(10))
                .Where(_ => !_client.IsConnectedOrConnecting)
                .Subscribe(_ => _client.Start());
        }

        public void WatchRepo(Repo repo)
        {
            _client.WatchRepo(repo);
        }

        public void UnWatchRepo(Repo repo)
        {
            _client.UnWatchRepo(repo);
        }

        public IObservable<string> RepoChangeStream { get; }
    }
}
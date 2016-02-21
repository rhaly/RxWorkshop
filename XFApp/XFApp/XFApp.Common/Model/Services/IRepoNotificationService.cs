using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace XFApp.Common.Model.Services
{
    public interface IRepoNotificationService
    {
         IObservable<string> RepoNotificationStream { get; }
    }

    public interface IRepoNotificationController
    {
        void NotifyRepoChanges(string repo);
    }

    public class RepoNotificationService : IRepoNotificationController, IRepoNotificationService
    {
        readonly ISubject<string> _repoStream = new Subject<string>();

        public void NotifyRepoChanges(string repo)
        {
            _repoStream.OnNext(repo);
        }

        public IObservable<string> RepoNotificationStream => _repoStream.AsObservable();
    }
}
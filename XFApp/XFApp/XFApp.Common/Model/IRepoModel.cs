using GHApp.Contracts.Dto;

namespace XFApp.Common.Model
{
    public interface IRepoModel : IReactiveObject
    {
        Repo Dto { get; }

        bool IsWatched { get; set; }
    }

    class RepoModel : ReactiveObject, IRepoModel
    {
        public RepoModel(Repo repo)
        {
            Dto = repo;
        }
        public Repo Dto { get; }

        private bool _isWatched;

        public bool IsWatched
        {
            get { return _isWatched; }
            set
            {
                _isWatched = value;
                OnPropertyChanged();
            }
        }
    }
}
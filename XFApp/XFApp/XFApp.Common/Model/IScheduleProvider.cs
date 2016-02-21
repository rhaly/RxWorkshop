using System;
using System.Reactive.Concurrency;

namespace XFApp.Common.Model
{
    public interface IScheduleProvider
    {
        IScheduler CurrentThread { get; }

        IScheduler UiScheduler { get; }

        IScheduler Immediate { get; }

        IScheduler NewThread { get; }

        IScheduler TaskPool { get; }
    }

    public sealed class ScheduleProvider : IScheduleProvider
    {
        private static readonly IScheduler MainThreadScheduler;               

        public IScheduler CurrentThread => Scheduler.CurrentThread;

        public IScheduler UiScheduler => MainThreadScheduler;

        public IScheduler Immediate => Scheduler.Immediate;

        public IScheduler NewThread => NewThreadScheduler.Default;

        public IScheduler TaskPool => TaskPoolScheduler.Default;

        static ScheduleProvider()
        {
            MainThreadScheduler = (IScheduler)DefaultScheduler.Instance;
        }
    }
}
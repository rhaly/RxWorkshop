using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using Prism.Mvvm;

namespace XFApp.Common.Model
{
    public interface IReactiveObject : INotifyPropertyChanged
    {
        IObservable<string> PropertyChangedStream { get; }
    }

    public class ReactiveObject : BindableBase, IReactiveObject
    {
        private readonly Subject<string> _subject = new Subject<string>();

        protected ReactiveObject()
        {
            Observable
                .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                    h => PropertyChanged += h,
                    h => PropertyChanged -= h)
                .Select(x => x.EventArgs.PropertyName).Subscribe(_subject.OnNext);
        }


        public IObservable<string> PropertyChangedStream => _subject.AsObservable();

    }
}
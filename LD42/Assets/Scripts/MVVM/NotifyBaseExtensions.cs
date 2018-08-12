using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Assets.Scripts.MVVM;
using UniRx;

namespace MVVM
{
    public static class NotifyBaseExtensions
    {
        public static IObservable<TType> ObserveProperty<T,TType>(this T source, Expression<Func<T, TType>> prop)
            where T : INotifyPropertyChanged
        {
            return Observable.Create<TType>(o =>
            {
                var propName = prop.GetPropertyInfo().Name;
                var propFunc = prop.Compile();
                
                return Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                        handler => handler.Invoke,
                        h => source.PropertyChanged += h,
                        h => source.PropertyChanged -= h
                    ).Where(e => e.EventArgs.PropertyName == propName)
                    .Select(e => propFunc(source))
                    .Subscribe(o);
            });
        }
    }
}

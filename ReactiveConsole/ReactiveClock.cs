using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace ReactiveConsole
{
    public class ReactiveClock
    {
        public static IObservable<DateTimeOffset> Start(IScheduler scheduler) => Observable.Interval(TimeSpan.FromSeconds(1), scheduler)
            .Select(_ => DateTimeOffset.Now);
    }
}
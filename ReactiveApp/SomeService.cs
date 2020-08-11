using System;
using System.Reactive.Linq;

namespace ReactiveApp
{
    public class SomeService : ISomeService
    {
        public IObservable<string> ExecuteSomeQuery() => MakeResultInFuture(TimeSpan.FromMilliseconds(500), "result1")
            .Merge(MakeResultInFuture(TimeSpan.FromSeconds(1), "result2"))
            .Merge(MakeResultInFuture(TimeSpan.FromMilliseconds(1500), "result3"));

        private IObservable<string> MakeResultInFuture(TimeSpan dueTime, string payload) => Observable.Timer(dueTime).Select(_ => payload);
    }
}
using System;

namespace ReactiveApp
{
    public interface ISomeService
    {
        IObservable<string> ExecuteSomeQuery();
    }
}
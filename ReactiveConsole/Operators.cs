using System;
using System.Reactive.Linq;

namespace ReactiveConsole
{
    public static class Operators
    {
        public static IDisposable Dump<T>(this IObservable<T> observable, string name) => observable.Subscribe(
            it => Console.WriteLine($"{name}: {it}"),
            ex => Console.WriteLine($"{name}: {ex}"),
            () => Console.WriteLine($"{name}: completed"));

        public static IObservable<string> Tokenize(this IObservable<char> input) => Observable.Create<string>(observer =>
        {
            var current = string.Empty;

            return input.Subscribe(it =>
            {
                if (it == ';')
                {
                    observer.OnNext(current);
                    current = string.Empty;

                }
                else
                {
                    current = current + it;
                }
            });
        });
    }
}
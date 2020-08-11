using System;

namespace ReactiveConsole
{
    public static class Operators
    {
        public static IDisposable Dump<T>(this IObservable<T> observable, string name) => observable.Subscribe(it => Console.WriteLine($"{name}: {it}"),
            ex => Console.WriteLine($"{name}: {ex}"),
            () => Console.WriteLine($"{name}: completed"));
    }
}
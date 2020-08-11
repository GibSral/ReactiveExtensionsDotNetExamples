using System;

namespace ReactiveConsole
{
    public static class Operators
    {
        public static void Dump<T>(IObservable<T> observable, string name) => observable.Subscribe(it => Console.WriteLine($"{name}: {it.ToString()}"),
            ex => Console.WriteLine($"{name}: {ex.Message}"),
            () => Console.WriteLine($"{name}: completed"));
    }
}
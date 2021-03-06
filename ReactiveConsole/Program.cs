﻿using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Windows.Forms.VisualStyles;

namespace ReactiveConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            OwnOperatorExample();
            Console.ReadKey();
        }

        private static void EnumerableAsObservableExample()
        {
            var observable = Enumerable.Range(0, 100)
                .ToObservable();
            observable
                .Subscribe(
                    it => Console.WriteLine($"{it}"),
                    ex => Console.WriteLine($"{ex}"),
                    () => Console.WriteLine("completed"));
            observable
                .Subscribe(
                    it => Console.WriteLine($"{it}"),
                    ex => Console.WriteLine($"{ex}"),
                    () => Console.WriteLine("completed"));
        }

        private static void SubjectExample()
        {
            var input = new Subject<string>();
            input.Dump("input");
            while (true)
            {
                var readLine = Console.ReadLine();
                if (readLine.Equals("exit"))
                {
                    input.OnCompleted();
                    break;
                }

                if (readLine.Equals("error"))
                {
                    input.OnError(new InvalidOperationException("Ich glaube nicht Tim"));
                }
                else
                {
                    input.OnNext(readLine);
                }
            }

            Console.WriteLine("Exited");
        }

        private static void EventHandlerExample()
        {
            var classWithEvent = new ClassWithEvent();
            var subscription = Observable.FromEventPattern<SomeEventArgs>(handler => classWithEvent.SomeEvent += handler, handler => classWithEvent.SomeEvent -= handler)
                .Select(it => it.EventArgs.Value)
                .Dump("event example");
            while (true)
            {
                var readLine = Console.ReadLine();
                if (readLine.Equals("exit"))
                {
                    subscription.Dispose();
                    break;
                }

                classWithEvent.TriggerEvent(readLine);
            }

            Console.WriteLine("Exited");
        }

        private static void AsyncToObservableExample()
        {
            var classWithAsyncMethod = new ClassWithAsyncMethod();
            Console.WriteLine("Starting long operation");
            var subscription = Observable.FromAsync(classWithAsyncMethod.WithResultAsync).Dump("async example");
            Console.ReadKey();
            subscription.Dispose();
        }

        private static void AsyncToObservableWithExceptionExample()
        {
            var classWithAsyncMethod = new ClassWithAsyncMethod();
            Console.WriteLine("Starting long operation");
            Observable.FromAsync(classWithAsyncMethod.WithException).Dump("async example");
        }

        private static void RandomNumbersExample()
        {
            var randomNumberStream1 = CreateRandomNumbers(1).Select(it => $"source 1: {it}").SubscribeOn(Scheduler.Default);
            var randomNumberStream2 = CreateRandomNumbers(2).Select(it => $"source 2: {it}").SubscribeOn(Scheduler.Default);
            randomNumberStream1.Merge(randomNumberStream2).Dump("merged random streams");
        }

        private static IObservable<int> CreateRandomNumbers(int seed) => Observable.Create<int>(observer =>
        {
            var running = true;
            var random = new Random(seed);
            while (running)
            {
                var next = random.Next();
                observer.OnNext(next);
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            return Disposable.Create(() => running = false);
        });

        private static void ReactiveClockExample()
        {
            var clockSubscription = ReactiveClock.Start(Scheduler.Default).Dump("clock");
            Console.ReadKey();
            clockSubscription.Dispose();
            Console.ReadKey();
        }

        private static void ColdObservableExample()
        {
            var timer = Observable.Interval(TimeSpan.FromSeconds(1));
            timer.Dump("timer 0");
            Console.ReadKey();
            timer.Dump("timer 1");
        }

        private static void PublishAndConnectExample()
        {
            var hotObservable = Observable.Interval(TimeSpan.FromSeconds(1)).Publish();
            hotObservable.Dump("hot observable 0");
            hotObservable.Dump("hot observable 1");
            Console.ReadKey();
            hotObservable.Connect();
            Thread.Sleep(3000);
            hotObservable.Dump("hot observable 2");
        }

        private static void OwnOperatorExample()
        {
            var input = new Subject<char>();
            input.Tokenize().Dump("own tokenizer");
            while (true)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }

                input.OnNext(key.KeyChar);
            }
        }
    }
}
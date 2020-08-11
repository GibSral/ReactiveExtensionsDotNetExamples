using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ReactiveConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SubjectExample();
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
            Console.ReadKey();
        }

        private static void ReactiveClockExample()
        {
            var clockSubscription = ReactiveClock.Start(Scheduler.Default).Dump("clock");
            Console.ReadKey();
            clockSubscription.Dispose();
            Console.ReadKey();
        }
    }
}
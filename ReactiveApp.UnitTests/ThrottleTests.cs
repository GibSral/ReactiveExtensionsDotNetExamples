using System;
using System.Reactive.Subjects;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using FluentAssertions;
using Microsoft.Reactive.Testing;
using NUnit.Framework;

namespace ReactiveApp.UnitTests
{
    [TestFixture]
    public class ThrottleTests : ReactiveTest
    {
        [Test]
        public void ThrottleInput_Always_ThrottlesInputCorrectly()
        {
            var testScheduler = new TestScheduler();
            var input = new Subject<string>();
            var throttledInput = input.Throttle(TimeSpan.FromTicks(2), testScheduler);
            testScheduler.Schedule(TimeSpan.FromTicks(Subscribed + 1), () => input.OnNext("1"));
            testScheduler.Schedule(TimeSpan.FromTicks(Subscribed + 2), () => input.OnNext("2"));
            testScheduler.Schedule(TimeSpan.FromTicks(Subscribed + 3), () => input.OnNext("3"));
            testScheduler.Schedule(TimeSpan.FromTicks(Subscribed + 10), () => input.OnNext("4"));
            testScheduler.Schedule(TimeSpan.FromTicks(Subscribed + 11), () => input.OnNext("5"));
            testScheduler.Schedule(TimeSpan.FromTicks(Subscribed + 20), () => input.OnNext("6"));
            testScheduler.Schedule(TimeSpan.FromTicks(Subscribed + 21), () => input.OnCompleted());

            var testableObserver = testScheduler.Start(() => throttledInput, Created, Subscribed, Disposed);

            var expectedMessage = new[]
            {
                OnNext(Subscribed + 5, "3"),
                OnNext(Subscribed + 13, "5"),
                OnNext(Subscribed + 21, "6"),
                OnCompleted<string>(Subscribed + 21)
            };
            testableObserver.Messages.Should().BeEquivalentTo(expectedMessage, options => options.WithStrictOrdering());
        }
    }
}
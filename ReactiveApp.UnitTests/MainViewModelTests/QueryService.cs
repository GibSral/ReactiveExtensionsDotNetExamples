using System.Reactive.Subjects;
using FluentAssertions;
using Microsoft.Reactive.Testing;
using NSubstitute;

namespace ReactiveApp.UnitTests.MainViewModelTests
{
    using System.Diagnostics.CodeAnalysis;

    using NUnit.Framework;

    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Special NamingConvention for tests")]
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Special NamingConvention for tests")]
    public class QueryService
    {
        [Test]
        public void QueryService_Always_UpdatesViewModelOnUiThread()
        {
            var uiThreadScheduler = new TestScheduler();
            var queryResult = new Subject<string>();
            var someService = Substitute.For<ISomeService>();
            someService.ExecuteSomeQuery().Returns(queryResult);
            var systemUnderTest = new MainViewModel(uiThreadScheduler, someService);

            systemUnderTest.ExecuteQuery.Execute(null);

            queryResult.OnNext("result1");
            uiThreadScheduler.AdvanceBy(1);
            systemUnderTest.QueryResults.Should().BeEquivalentTo(new[] { "result1" });

            queryResult.OnNext("result2");
            queryResult.OnNext("result3");
            queryResult.OnNext("result4");
            uiThreadScheduler.AdvanceBy(1);
            systemUnderTest.QueryResults.Should().BeEquivalentTo(new[] { "result1", "result2" });

            uiThreadScheduler.AdvanceBy(1);
            systemUnderTest.QueryResults.Should().BeEquivalentTo(new[] { "result1", "result2", "result3" });

            uiThreadScheduler.AdvanceBy(1);
            systemUnderTest.QueryResults.Should().BeEquivalentTo(new[] { "result1", "result2", "result3", "result4" });
        } 
    }
}
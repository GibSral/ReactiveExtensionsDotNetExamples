using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReactiveConsole
{
    public class ClassWithAsyncMethod
    {

        public Task<string> WithResultAsync(CancellationToken cancellationToken) => Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken).ConfigureAwait(false);
            if (!cancellationToken.IsCancellationRequested)
            {
                return "SomeAsyncResult";
            }

            throw new TaskCanceledException();
        }, cancellationToken);

        public Task<string> WithException() => throw new InvalidOperationException("Error during async execution");
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetToolbox.Helpers
{
    public static class Retry
    {
        public static T Execute<T>(Func<T> action, int maxRetries = 3, int delayMs = 500)
        {
            Exception lastException = null;
            for (int i = 0; i <= maxRetries; i++)
            {
                try
                {
                    return action();
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    if (i < maxRetries)
                        Thread.Sleep(delayMs * (i + 1));
                }
            }
            throw lastException;
        }

        public static async Task<T> ExecuteAsync<T>(Func<Task<T>> action, int maxRetries = 3, int delayMs = 500)
        {
            Exception lastException = null;
            for (int i = 0; i <= maxRetries; i++)
            {
                try
                {
                    return await action();
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    if (i < maxRetries)
                        await Task.Delay(delayMs * (i + 1));
                }
            }
            throw lastException;
        }

        public static void Execute(Action action, int maxRetries = 3, int delayMs = 500)
        {
            Execute<object>(() => { action(); return null; }, maxRetries, delayMs);
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;

namespace TrueOrFalse.Tests
{
    public static class ThreadHelper
    {
        public static Task StartStaTask(Action action)
        {
            var tcs = new TaskCompletionSource<object>();
            var thread = new Thread(() =>
            {
                try
                {
                    action();
                    tcs.SetResult(new object());
                }
                catch (Exception exception)
                {
                    tcs.SetException(exception);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }
    }
}

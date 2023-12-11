namespace TaskCancellation
{
    internal class Program
    {
        static void ThreadProc(object? token)
        {
            CancellationToken? cancellationToken = (CancellationToken?)token;
            while (!(cancellationToken?.IsCancellationRequested ?? true))
            {
                Console.WriteLine("ThreadMethod Working...");
                Thread.Sleep(100);
            }
        }

        static void ThreadAsync()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            Thread thread = new Thread(new ParameterizedThreadStart(ThreadProc));
            thread.Start(cancellationTokenSource.Token);
            cancellationTokenSource.CancelAfter(500);
        }

        static async Task TaskCannotCancellationAsync()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;
            Task task = Task.Run(() =>
            {

                Console.WriteLine("Task Can not Cancellation Running...");
                Thread.Sleep(1000);
                Console.WriteLine("Task Can not Cancellation Running...");
                Console.WriteLine("Task Finished");

            }, cancellationTokenSource.Token);

            cancellationTokenSource.Token.Register(() => Console.WriteLine("Cancel Task."));
            cancellationTokenSource.CancelAfter(500);
            
            await task;
        }

        static async Task TaskCancellationAsync()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;
            Task task = Task.Run(async () =>
            {

                token.ThrowIfCancellationRequested();

                bool moreToDo = true;
                while (moreToDo)
                {
                    Console.WriteLine("Task Cancellation Running...");
                    if (token.IsCancellationRequested)
                    {
                        token.ThrowIfCancellationRequested();
                    }
                    await Task.Delay(1000);
                }
                await Task.Delay(1000);
                Console.WriteLine("Task Finished");

            }, cancellationTokenSource.Token);

            cancellationTokenSource.Token.Register(() => Console.WriteLine("Cancel Task."));
            cancellationTokenSource.CancelAfter(500);

            try
            {
                await task;
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {e.Message}");
            }
            finally
            {
                cancellationTokenSource.Dispose();
            }
        }


        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, Task Canellation!");
            ThreadAsync();
            await TaskCannotCancellationAsync();
            Console.WriteLine("Task Canelled not effected.");
            await TaskCancellationAsync();
        }
    }
}
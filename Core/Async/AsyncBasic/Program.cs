namespace AsyncBasic
{
    internal class Program
    {

        static async Task BasicFuncAsync()
        {
            await Task.Run(
                () => Console.WriteLine("Hello Basic Func Async.")
            );
        }

        static async Task LoopFuncAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Loop func {i}.");
                await Task.Delay(100);
            }
        }

        static async Task FooAsync()
        {
            Console.WriteLine("Foo.");
        }

        static async Task<int> BasicFuncWithRetAsync()
        {
            Console.WriteLine("Basic func with return value.");
            await Task.Delay(1000);
            return 10086;
        }

        static async IAsyncEnumerable<int> GenerateEnumerableAsync()
        {
            for (int i = 0; i < 10; ++i)
            {
                yield return i;
                await Task.Delay(100);
            }
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Async Basic.");
            Task task1 = LoopFuncAsync();
            Task task2 = BasicFuncAsync();
            Task<int> task3 = BasicFuncWithRetAsync();
            Task.WaitAny(task2, task3);
            Console.WriteLine($"Basic Func Async ret value: {task3.Result}");
            Task.WaitAll(task1, task2, task3);

            Console.WriteLine($"Generate Enumerable Async");
            await foreach (int i in GenerateEnumerableAsync())
            {
                Console.WriteLine($"Async Enumerable: {i}");
            }

        }
    }
}
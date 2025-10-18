using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ComputerSystem.Common; //

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== Паралельна обробка компонентів ===");

        string filePath = "processors.json";
        var crud = new GenericCrudServiceAsync<Processor>(filePath);

        object lockObj = new();
        var semaphore = new SemaphoreSlim(4);

        var random = new Random();
        var processors = Enumerable.Range(0, 5000)
            .Select(_ => new Processor
            {
                Id = Guid.NewGuid(),
                Model = $"CPU-{random.Next(1000, 9999)}",
                Manufacturer = random.Next(2) == 0 ? "Intel" : "AMD",
                Cores = random.Next(2, 16),
                FrequencyGHz = Math.Round(random.NextDouble() * 3 + 1.5, 2)
            })
            .ToList();

        Console.WriteLine("Створення процесорів у паралельних потоках...");

        await Task.Run(() =>
        {
            Parallel.ForEach(processors, async processor =>
            {
                await semaphore.WaitAsync();
                try
                {
                    lock (lockObj)
                    {
                        crud.CreateAsync(processor).Wait();
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            });
        });

        Console.WriteLine("Усі 5000 процесорів створено.\n");

        var all = await crud.ReadAllAsync();

        double avgFreq = all.Average(p => p.FrequencyGHz);
        double minFreq = all.Min(p => p.FrequencyGHz);
        double maxFreq = all.Max(p => p.FrequencyGHz);

        Console.WriteLine($"Статистика процесорів:");
        Console.WriteLine($"Мінімальна частота: {minFreq:F2} GHz");
        Console.WriteLine($"Максимальна частота: {maxFreq:F2} GHz");
        Console.WriteLine($"Середня частота: {avgFreq:F2} GHz\n");

        if (await crud.SaveAsync())
            Console.WriteLine($"Дані збережено у файл: {filePath}");
        else
            Console.WriteLine("Помилка збереження даних.");

        Console.WriteLine("\nНатисніть Enter для завершення...");
        Console.ReadLine();
    }
}

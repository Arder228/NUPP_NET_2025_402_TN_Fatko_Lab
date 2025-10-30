using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ComputerSystem.Infrastructure;
using ComputerSystem.Infrastructure.Repositories;
using ComputerSystem.Infrastructure.Models;
using ComputerSystem.Common;
using ComputerSystem.Infrastructure.Services;
using ComputerSystem.Nosql.Models;

class Program
{
    static async Task Main(string[] args)
    {
        /*
        var services = new ServiceCollection();

        var dbPath = "data/computersystem.db";
        var folder = Path.GetDirectoryName(dbPath);
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        var connectionString = $"Data Source={dbPath}";
        services.AddDbContext<ComputerSystemContext>(opts =>
            opts.UseSqlite(connectionString));

        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(ICrudServiceAsync<>), typeof(CrudServiceWithRepo<>));

        var serviceProvider = services.BuildServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var ctx = scope.ServiceProvider.GetRequiredService<ComputerSystemContext>();
            ctx.Database.EnsureCreated();
        }

        using (var scope = serviceProvider.CreateScope())
        {
            var compService = scope.ServiceProvider.GetRequiredService<ICrudServiceAsync<ComputerModel>>();
            var vendorRepo = scope.ServiceProvider.GetRequiredService<IRepository<VendorModel>>();

            var vendor = new VendorModel
            {
                Name = "Noctua",
                Country = "Austria"
            };
            await vendorRepo.AddAsync(vendor);

            var computer = new ComputerModel { Name = "Workstation-1" };

            computer.Components.Add(new ProcessorModel
            {
                Model = "Ryzen 9 5900X",
                Manufacturer = "AMD",
                Cores = 12,
                BaseFrequencyGhz = 3.7,
                BoostFrequencyGhz = 4.8,
                Vendor = vendor
            });

            computer.Components.Add(new GraphicsCardModel
            {
                Model = "RTX 4080",
                Manufacturer = "NVIDIA",
                MemoryGB = 16,
                CoreClockGhz = 2.2,
                Vendor = vendor
            });

            await compService.CreateAsync(computer);

            var allComputers = (await compService.ReadAllAsync()).ToList();
            Console.WriteLine($"Computers in DB: {allComputers.Count}");
            foreach (var c in allComputers)
                Console.WriteLine($" - {c.Name} (Id={c.Id})");

            await compService.SaveAsync();
        }

        Console.WriteLine("Готово. Натисніть Enter для виходу...");
        Console.ReadLine();
        */

        var connectionString = "mongodb+srv://admin:admin@cluster0.cr8cu6k.mongodb.net/myVirtualDatabase?retryWrites=true&w=majority";


        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("myVirtualDatabase");
        var computersCollection = database.GetCollection<ComputerDocument>("Computers");

        var computersToAdd = new List<ComputerDocument>();

        for (int i = 1; i <= 5; i++)
        {
            computersToAdd.Add(new ComputerDocument
            {
                Id = Guid.NewGuid(),
                Name = $"Computer-{i}",
                CreatedAt = DateTime.UtcNow
            });
        }

        await computersCollection.InsertManyAsync(computersToAdd);
        Console.WriteLine($"{computersToAdd.Count} комп'ютери додано!");

        var allComputers = await computersCollection.Find(FilterDefinition<ComputerDocument>.Empty).ToListAsync();
        Console.WriteLine("Всі комп'ютери у колекції:");
        foreach (var comp in allComputers)
        {
            Console.WriteLine($"Id: {comp.Id}, Name: {comp.Name}, CreatedAt: {comp.CreatedAt:dd.MM.yyyy HH:mm}");
        }
    }
}

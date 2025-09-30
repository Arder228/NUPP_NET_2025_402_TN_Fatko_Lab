using System;
using ComputerSystem.Common;

namespace ComputerSystem.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // dикористовуємо GenericCrudService
            var crudService = new GenericCrudService<Component>();

            // cтворюємо кілька об’єктів
            var cpu = new Processor("Ryzen 7 7800X3D", "AMD", 8, 4.2, 5.0);
            var gpu = new GraphicsCard("RTX 4080", "NVIDIA", 16, 2.6);
            var ram = new Memory("Kingston Fury", "Kingston", 32, "DDR5");

            crudService.Create(cpu);
            crudService.Create(gpu);
            crudService.Create(ram);

            Console.WriteLine("\n--- Всі додані елементи ---");
            foreach (var item in crudService.ReadAll())
            {
                Console.WriteLine(item.GetInfo());
            }

            // демонстрація Update
            cpu.Overclock(5.1);
            crudService.Update(cpu);

            Console.WriteLine("\n--- Після оновлення CPU ---");
            Console.WriteLine(crudService.Read(cpu.Id).GetInfo());

            // Remove
            crudService.Remove(ram);

            Console.WriteLine("\n--- Після видалення RAM ---");
            foreach (var item in crudService.ReadAll())
            {
                Console.WriteLine(item.GetInfo());
            }

            // статичний метод
            Console.WriteLine("\nКількість створених компонентів: " + Component.GetCreatedCount());

            // метод-розширення
            Console.WriteLine("\nGPU опис (метод-розширення): " + gpu.ToShortString());

            // збереження / завантаження
            const string filePath = "components.json";
            crudService.Save(filePath);
            Console.WriteLine($"\nДані збережено у {filePath}");

            var newCrudService = new GenericCrudService<Component>();
            newCrudService.Load(filePath);
            Console.WriteLine("\n--- Завантажені з файлу дані ---");
            foreach (var item in newCrudService.ReadAll())
            {
                Console.WriteLine(item.GetInfo());
            }

            Console.WriteLine("\n--- Кінець демонстрації ---");
        }
    }
}
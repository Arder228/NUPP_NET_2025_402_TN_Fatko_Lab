using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using ComputerSystem.Common;

namespace ComputerSystem.Common.Tests
{
    public class CrudServiceAsyncTests : IDisposable
    {
        private readonly GenericCrudServiceAsync<Component> _crudService;
        private readonly string _tempFilePath;

        public CrudServiceAsyncTests()
        {
            _tempFilePath = Path.GetTempFileName();
            _crudService = new GenericCrudServiceAsync<Component>(_tempFilePath);
        }

        public void Dispose()
        {
            if (File.Exists(_tempFilePath))
                File.Delete(_tempFilePath);
        }

        [Fact]
        public async Task CreateAsync_AddsNewComponent()
        {
            var processor = new Processor("Ryzen 7 5800X", "AMD", 8, 3.8, 4.7);
            await _crudService.CreateAsync(processor);

            var all = await _crudService.ReadAllAsync();

            Assert.Single(all);
            Assert.Contains(processor, all);
        }

        [Fact]
        public async Task ReadAsync_ReturnsExistingComponent()
        {
            var gpu = new GraphicsCard("RTX 4080", "NVIDIA", 16, 2.6);
            await _crudService.CreateAsync(gpu);

            var result = await _crudService.ReadAsync(gpu.Id);

            Assert.NotNull(result);
            Assert.Equal("RTX 4080", result!.Model);
        }

        [Fact]
        public async Task UpdateAsync_ChangesComponent()
        {
            var ram = new Memory("Vengeance", "Corsair", 16, "DDR5");
            await _crudService.CreateAsync(ram);

            ram.SizeGB = 32;
            await _crudService.UpdateAsync(ram);

            var updated = await _crudService.ReadAsync(ram.Id);
            Assert.NotNull(updated);
            Assert.Equal(32, ((Memory)updated!).SizeGB);
        }

        [Fact]
        public async Task DeleteAsync_RemovesComponent()
        {
            var cooler = new Cooling("NH-D15", "Noctua", "Air", 2, 250);
            await _crudService.CreateAsync(cooler);

            await _crudService.RemoveAsync(cooler);

            var result = await _crudService.ReadAsync(cooler.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task ReadAllAsync_ReturnsAllItems()
        {
            var components = new List<Component>
            {
                new Processor("i7-12700K", "Intel", 12, 3.6, 5.0),
                new Memory("Fury", "Kingston", 16, "DDR4"),
                new PowerSupply("RM850x", "Corsair", 850, "80+ Gold")
            };

            foreach (var c in components)
                await _crudService.CreateAsync(c);

            var all = await _crudService.ReadAllAsync();

            Assert.Equal(3, all is ICollection<Component> collection ? collection.Count : 0);
        }
    }
}

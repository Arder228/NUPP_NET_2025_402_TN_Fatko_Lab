using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ComputerSystem.Common;
using ComputerSystem.Infrastructure.Models;
using ComputerSystem.REST.Models;
using System.ComponentModel.DataAnnotations;

namespace ComputerSystem.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComputersController : ControllerBase
    {
        private readonly ICrudServiceAsync<ComputerModel> _computerService;
        private readonly ILogger<ComputersController> _logger;

        public ComputersController(ICrudServiceAsync<ComputerModel> computerService, ILogger<ComputersController> logger)
        {
            _computerService = computerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComputerDto>>> GetComputers(
            [FromQuery] int page = 1, 
            [FromQuery] int amount = 10)
        {
            try
            {
                var computers = await _computerService.ReadAllAsync(page, amount);
                var computerDtos = computers.Select(MapToDto);
                return Ok(computerDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting computers");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ComputerDto>> GetComputer(int id)
        {
            try
            {
                var computer = await _computerService.ReadAsync(id);
                if (computer == null)
                {
                    return NotFound();
                }

                return MapToDto(computer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting computer with id {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "User,Librarian,Admin")]
        public async Task<ActionResult<ComputerDto>> CreateComputer(CreateComputerRequest request)
        {
            try
            {
                var computer = new ComputerModel
                {
                    Name = request.Name,
                    CreatedAt = DateTime.UtcNow
                };

                var created = await _computerService.CreateAsync(computer);
                if (!created)
                {
                    return BadRequest("Failed to create computer");
                }

                await _computerService.SaveAsync();

                var computerDto = MapToDto(computer);
                return CreatedAtAction(nameof(GetComputer), new { id = computer.Id }, computerDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating computer");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Librarian,Admin")]
        public async Task<IActionResult> UpdateComputer(int id, UpdateComputerRequest request)
        {
            try
            {
                var computer = await _computerService.ReadAsync(id);
                if (computer == null)
                {
                    return NotFound();
                }

                computer.Name = request.Name;

                var updated = await _computerService.UpdateAsync(computer);
                if (!updated)
                {
                    return BadRequest("Failed to update computer");
                }

                await _computerService.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating computer with id {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteComputer(int id)
        {
            try
            {
                var computer = await _computerService.ReadAsync(id);
                if (computer == null)
                {
                    return NotFound();
                }

                var deleted = await _computerService.RemoveAsync(computer);
                if (!deleted)
                {
                    return BadRequest("Failed to delete computer");
                }

                await _computerService.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting computer with id {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        private ComputerDto MapToDto(ComputerModel computer)
        {
            return new ComputerDto
            {
                Id = computer.Id,
                Name = computer.Name,
                CreatedAt = computer.CreatedAt,
                Components = computer.Components.Select(MapToDto).ToList()
            };
        }
				
        private ComponentDto MapToDto(ComponentModel component)
        {
            return component switch
            {
                ProcessorModel processor => new ProcessorDto
                {
                    Id = processor.Id,
                    Uid = processor.Uid,
                    Model = processor.Model,
                    Manufacturer = processor.Manufacturer,
                    Cores = processor.Cores,
                    BaseFrequencyGhz = processor.BaseFrequencyGhz,
                    BoostFrequencyGhz = processor.BoostFrequencyGhz,
                    VendorId = processor.VendorId,
                    VendorName = processor.Vendor?.Name
                },
                GraphicsCardModel gpu => new GraphicsCardDto
                {
                    Id = gpu.Id,
                    Uid = gpu.Uid,
                    Model = gpu.Model,
                    Manufacturer = gpu.Manufacturer,
                    MemoryGB = gpu.MemoryGB,
                    CoreClockGhz = gpu.CoreClockGhz,
                    VendorId = gpu.VendorId,
                    VendorName = gpu.Vendor?.Name
                },
                MemoryModel memory => new MemoryDto
                {
                    Id = memory.Id,
                    Uid = memory.Uid,
                    Model = memory.Model,
                    Manufacturer = memory.Manufacturer,
                    SizeGB = memory.SizeGB,
                    Type = memory.Type,
                    VendorId = memory.VendorId,
                    VendorName = memory.Vendor?.Name
                },
                CoolingModel cooling => new CoolingDto
                {
                    Id = cooling.Id,
                    Uid = cooling.Uid,
                    Model = cooling.Model,
                    Manufacturer = cooling.Manufacturer,
                    Type = cooling.Type,
                    FanCount = cooling.FanCount,
                    MaxTdpWatts = cooling.MaxTdpWatts,
                    VendorId = cooling.VendorId,
                    VendorName = cooling.Vendor?.Name
                },
                MotherboardModel motherboard => new MotherboardDto
                {
                    Id = motherboard.Id,
                    Uid = motherboard.Uid,
                    Model = motherboard.Model,
                    Manufacturer = motherboard.Manufacturer,
                    Chipset = motherboard.Chipset,
                    FormFactor = motherboard.FormFactor,
                    Socket = motherboard.Socket,
                    VendorId = motherboard.VendorId,
                    VendorName = motherboard.Vendor?.Name
                },
                PowerSupplyModel psu => new PowerSupplyDto
                {
                    Id = psu.Id,
                    Uid = psu.Uid,
                    Model = psu.Model,
                    Manufacturer = psu.Manufacturer,
                    PowerWatts = psu.PowerWatts,
                    EfficiencyRating = psu.EfficiencyRating,
                    VendorId = psu.VendorId,
                    VendorName = psu.Vendor?.Name
                },
                _ => CreateBaseComponentDto(component)
            };
        }

        private ComponentDto CreateBaseComponentDto(ComponentModel component)
        {
            return new MemoryDto
            {
                Id = component.Id,
                Uid = component.Uid,
                Model = component.Model,
                Manufacturer = component.Manufacturer,
                VendorId = component.VendorId,
                VendorName = component.Vendor?.Name,
                SizeGB = 0,
                Type = "Unknown"
            };
        }
    }
}
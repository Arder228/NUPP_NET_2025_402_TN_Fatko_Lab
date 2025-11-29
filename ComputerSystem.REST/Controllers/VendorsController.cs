using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ComputerSystem.Common;
using ComputerSystem.Infrastructure.Models;
using ComputerSystem.REST.Models;

namespace ComputerSystem.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorsController : ControllerBase
    {
        private readonly ICrudServiceAsync<VendorModel> _vendorService;
        private readonly ILogger<VendorsController> _logger;

        public VendorsController(ICrudServiceAsync<VendorModel> vendorService, ILogger<VendorsController> logger)
        {
            _vendorService = vendorService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendorDto>>> GetVendors(
            [FromQuery] int page = 1, 
            [FromQuery] int amount = 10)
        {
            try
            {
                var vendors = await _vendorService.ReadAllAsync(page, amount);
                var vendorDtos = vendors.Select(MapToDto);
                return Ok(vendorDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting vendors");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VendorDto>> GetVendor(int id)
        {
            try
            {
                var vendor = await _vendorService.ReadAsync(id);
                if (vendor == null)
                {
                    return NotFound();
                }

                return MapToDto(vendor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting vendor with id {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Librarian,Admin")]
        public async Task<ActionResult<VendorDto>> CreateVendor(CreateVendorRequest request)
        {
            try
            {
                var vendor = new VendorModel
                {
                    Name = request.Name,
                    Country = request.Country
                };

                var created = await _vendorService.CreateAsync(vendor);
                if (!created)
                {
                    return BadRequest("Failed to create vendor");
                }

                await _vendorService.SaveAsync();

                var vendorDto = MapToDto(vendor);
                return CreatedAtAction(nameof(GetVendor), new { id = vendor.Id }, vendorDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating vendor");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Librarian,Admin")]
        public async Task<IActionResult> UpdateVendor(int id, UpdateVendorRequest request)
        {
            try
            {
                var vendor = await _vendorService.ReadAsync(id);
                if (vendor == null)
                {
                    return NotFound();
                }

                vendor.Name = request.Name;
                vendor.Country = request.Country;

                var updated = await _vendorService.UpdateAsync(vendor);
                if (!updated)
                {
                    return BadRequest("Failed to update vendor");
                }

                await _vendorService.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating vendor with id {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            try
            {
                var vendor = await _vendorService.ReadAsync(id);
                if (vendor == null)
                {
                    return NotFound();
                }

                var deleted = await _vendorService.RemoveAsync(vendor);
                if (!deleted)
                {
                    return BadRequest("Failed to delete vendor");
                }

                await _vendorService.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting vendor with id {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        private VendorDto MapToDto(VendorModel vendor)
        {
            return new VendorDto
            {
                Id = vendor.Id,
                Name = vendor.Name,
                Country = vendor.Country
            };
        }
    }
}
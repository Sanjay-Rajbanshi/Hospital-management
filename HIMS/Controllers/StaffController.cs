using HIMS.Interfaces;
using HIMS.Model;
using Microsoft.AspNetCore.Mvc;


namespace HIMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStaff()
        {
            var staffs = await _staffService.GetAllStaffsAsync();
            return Ok(staffs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffById(Guid id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
            if (staff == null) return NotFound();
            return Ok(staff);
        }

        [HttpPost("Create Staff")]
        public async Task<IActionResult> AddStaff([FromBody] Staff staff)
        {
            var added = await _staffService.AddStaffAsync(staff);
            return CreatedAtAction(nameof(GetStaffById), new { id = added.Id }, added);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(Guid id, [FromBody] Staff staff)
        {
            if (id != staff.Id) return BadRequest("ID mismatch");
            var updated = await _staffService.UpdateStaffAsync(staff);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(Guid id)
        {
            var result = await _staffService.DeleteStaffAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        
    }
}

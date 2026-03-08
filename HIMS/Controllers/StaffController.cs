using HIMS.DTO;
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
            if (staff == null) return NotFound(new {message = "Staff not found"});
            return Ok(staff);
        }

        [HttpPost]
        public async Task<IActionResult> AddStaff([FromBody] StaffDto staffDto)
        {
            var staff = new Staff
            {
                Id = Guid.NewGuid(),
                Name = staffDto.Name,
                DateOfBirth = staffDto.DateOfBirth,
                Role = staffDto.Role,
                Department = staffDto.Department,
                PhoneNumber = staffDto.PhoneNumber
            };

            var added = await _staffService.AddStaffAsync(staff);
            // return CreatedAtAction(nameof(GetStaffById), new { id = added.Id }, added);
            return Ok(new { message = "Staff created successfully", data = added });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(Guid id, [FromBody] StaffDto staffDto)
        {
            var existing = await _staffService.GetStaffByIdAsync(id);
            if (existing == null) return NotFound();
            existing.Name = staffDto.Name;
            existing.DateOfBirth = staffDto.DateOfBirth;
            existing.Role = staffDto.Role;
            existing.Department = staffDto.Department;
            existing.PhoneNumber = staffDto.PhoneNumber;

            var updated = await _staffService.UpdateStaffAsync(existing);
            return Ok(new {message ="Staff updated successfuly", data = updated});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(Guid id)
        {
            var result = await _staffService.DeleteStaffAsync(id);
            if (!result) 
                return NotFound(new { message ="Staff not found"});
            return Ok(new { message = "Staff deleted successfully"});
        }

        
    }
}

using HIMS.DTOs;
using HIMS.Data;
using HIMS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HIMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatientController(AppDbContext context)
        {
            _context = context;
        }

        //  Create Patient
        [HttpPost("createpatient")]
        public async Task<IActionResult> Create(CreatePatientDto dto)
        {
           
           
                var patient = new Patient
                {
                    Id = Guid.NewGuid(),
                    Name = dto.Name,
                    Address = dto.Address,
                    PhoneNumber = dto.PhoneNumber,
                    DateOfBirth = dto.DateOfBirth,
                    Gender = dto.Gender
                };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return Ok(new {message = "Patient is created successfully" });

        }

        //  Get All Patients
        [HttpGet]
        public async Task<ActionResult<List<PatientDto>>> GetAll()
        {
            var patients = await _context.Patients
                .Select(p => new PatientDto
                {
                    Id = p.Id,
                    Name = p.Name ?? string.Empty,
                    Address = p.Address ?? string.Empty,
                   PhoneNumber = p.PhoneNumber ?? string.Empty,
                    DateOfBirth = p.DateOfBirth,    
                    Gender = p.Gender ?? string.Empty
                })
                 .ToListAsync();
            return Ok(patients);
        }

        //  Get Patient By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var patient = await _context.Patients
                .Where(p => p.Id == id)
                .Select(p => new PatientDto
                {
                    Id = p.Id,
                    Name = p.Name ?? string.Empty,
                    Address = p.Address ?? string.Empty,
                    DateOfBirth= p.DateOfBirth,
                    Gender = p.Gender ?? string.Empty,
                    PhoneNumber = p.PhoneNumber?? string.Empty
                })
                .FirstOrDefaultAsync();
            if (patient == null)
                return NotFound("Patient not found");   
            return Ok(patient);
        }

        //  Update Patient
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdatePatientDto dto)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (patient == null)
                return NotFound(new { message = "Patient not found" });
            patient.Name = dto.Name;
            patient.Address = dto.Address;
            patient.PhoneNumber = dto.PhoneNumber;
            patient.DateOfBirth = dto.DateOfBirth;
            patient.Gender = dto.Gender;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Patient updated successfully" });
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
                return NotFound(new { message = "Patient not found" });

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Patient deleted successfully" });
        }
    }
}

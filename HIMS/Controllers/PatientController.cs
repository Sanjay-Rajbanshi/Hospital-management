


using HIMS.Dbhospital;
using HIMS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HIMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly AppDb _context;

        public PatientController(AppDb context)
        {
            _context = context;
        }

        // ✅ Create Patient
        [HttpPost]
        public async Task<IActionResult> Create(Patient patient)
        {
            if (patient.Age <= 0)
                return BadRequest("Age must be greater than 0");

            patient.Id = Guid.NewGuid();   // generate Guid

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return Ok(patient);
        }

        // ✅ Get All Patients
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _context.Patients.ToListAsync();
            return Ok(patients);
        }

        // ✅ Get Patient By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(x => x.Id == id);

            if (patient == null)
                return NotFound("Patient not found");

            return Ok(patient);
        }

        // ✅ Update Patient
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Patient updatedPatient)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(x => x.Id == id);

            if (patient == null)
                return NotFound("Patient not found");

            patient.Name = updatedPatient.Name;
            patient.Address = updatedPatient.Address;
            patient.PhoneNumber = updatedPatient.PhoneNumber;
            patient.Age = updatedPatient.Age;
            patient.Problem = updatedPatient.Problem;

            await _context.SaveChangesAsync();
            return Ok(patient);
        }

        // ✅ Delete Patient
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(x => x.Id == id);

            if (patient == null)
                return NotFound("Patient not found");

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return Ok("Patient deleted successfully");
        }
    }
}

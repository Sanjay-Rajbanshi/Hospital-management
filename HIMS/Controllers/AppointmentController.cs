using HIMS.DTOs;
using HIMS.Interfaces;
using HIMS.Model;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();

            var result = appointments.Select(a => new AppointmentDto
            {
                Id = a.Id,
                PatientId = a.PatientId,
                PatientName = a.Patient?.Name ?? string.Empty,
                StaffId = a.StaffId,
                StaffName = a.Staff?.Name ?? string.Empty,
                StaffRole = a.Staff != null ? a.Staff.Role.ToString() : string.Empty,
                AppointmentDate = a.AppointmentDate,
                Status = a.Status,
                Notes = a.Notes
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            var a = await _appointmentService.GetAppointmentByIdAsync(id);
            if (a == null) return NotFound();

            var result = new AppointmentDto
            {
                Id = a.Id,
                PatientId = a.PatientId,
                PatientName = a.Patient?.Name ?? string.Empty,
                
                Notes = a.Notes
            };

            return Ok(result);
        }

        [HttpPost ("Create Appointment")]
        public async Task<IActionResult> BookAppointment([FromBody] CreateAppointmentDto dto)
        {
            try
            {
                var appointment = new Appointment
                {
                    Id = Guid.NewGuid(),
                    PatientId = dto.PatientId,
                    StaffId = dto.StaffId,
                    AppointmentDate = dto.AppointmentDate,
                    Status = "Booked",
                    Notes = dto.Notes ?? string.Empty
                };

                var added = await _appointmentService.AddAppointmentAsync(appointment);

                var result = new AppointmentDto
                {
                    Id = added.Id,
                    PatientId = added.PatientId,
                    PatientName = added.Patient?.Name ?? string.Empty,
                    StaffId = added.StaffId,
                    StaffName = added.Staff?.Name ?? string.Empty,
                    StaffRole = added.Staff != null ? added.Staff.Role.ToString() : string.Empty,
                    AppointmentDate = added.AppointmentDate,
                    Status = added.Status,
                    Notes = added.Notes
                };

                return CreatedAtAction(nameof(GetAppointmentById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(Guid id, [FromBody] CreateAppointmentDto dto)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null) return NotFound();

            appointment.PatientId = dto.PatientId;
            appointment.StaffId = dto.StaffId;
            appointment.AppointmentDate = dto.AppointmentDate;
            appointment.Notes = dto.Notes ?? string.Empty;

            var updated = await _appointmentService.UpdateAppointmentAsync(appointment);

            var result = new AppointmentDto
            {
                Id = updated.Id,
                PatientId = updated.PatientId,
                PatientName = updated.Patient?.Name ?? string.Empty,
                StaffId = updated.StaffId,
                StaffName = updated.Staff?.Name ?? string.Empty,
                StaffRole = updated.Staff != null ? updated.Staff.Role.ToString() : string.Empty,
                AppointmentDate = updated.AppointmentDate,
                Status = updated.Status,
                Notes = updated.Notes
            };

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            var deleted = await _appointmentService.DeleteAppointmentAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}

using HIMS.DTOs;
using HIMS.Interfaces;
using HIMS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
            try
            {
                var appointments = await _appointmentService.GetAllAppointmentsAsync();

                var result = appointments.Select(a => new AppointmentDto
                {
                    Id = a.Id,
                    PatientId = a.PatientId,
                    PatientName = a.Patient?.Name ?? "",

                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor?.Name ?? "",

                    AppointmentDateTime = a.AppointmentDateTime,
                    AppointmentStatus = a.AppointmentStatus,

                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, StackTrace = ex.Message });

            }
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
                PatientName = a.Patient?.Name ?? "",
                DoctorId = a.DoctorId,
                DoctorName = a.Doctor?.Name ?? "",
                AppointmentDateTime = a.AppointmentDateTime,
                AppointmentStatus = a.AppointmentStatus,



            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> BookAppointment([FromBody] CreateAppointmentDto dto)
        {
            if (dto.AppointmentDateTime < DateTime.Now)
            {
                return BadRequest("Appointment date cannot be in the past,");
            }


            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentDateTime = dto.AppointmentDateTime,
                AppointmentStatus = AppointmentStatus.Booked,

            };

            var added = await _appointmentService.AddAppointmentAsync(appointment);

            var result = new AppointmentDto
            {
                Id = added.Id,
                PatientId = added.PatientId,
                PatientName = added.Patient?.Name ?? "",
                DoctorId = added.DoctorId,
                DoctorName = added.Doctor?.Name ?? "",
                AppointmentDateTime = added.AppointmentDateTime,
                AppointmentStatus = added.AppointmentStatus,
            };

            return CreatedAtAction(nameof(GetAppointmentById), new { id = result.Id }, result); 
        }
            
         

        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var result = await _appointmentService.CancelAppointmentAsync(id);

            if (!result)
                return NotFound(new { message = "Appointment not found" });

            return Ok(new { message = "Appointment cancelled successfully." });
        }


        
    }
}

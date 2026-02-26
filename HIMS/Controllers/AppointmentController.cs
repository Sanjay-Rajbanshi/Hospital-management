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
        private readonly DbContext _context;

        public AppointmentController(IAppointmentService appointmentService, DbContext context)
        {
            _appointmentService = appointmentService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();

            var result = appointments.Select(a => new AppointmentDto
            {
                Id = a.Id,
                PatientId = a.PatientId,
                
                DoctorId = a.DoctorId,
              
                AppointmentDateTime = a.AppointmentDateTime,
               
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
            try
            {
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
                   
                    DoctorId = added.DoctorId,
                   
                    AppointmentDateTime = added.AppointmentDateTime,
                  
                };

                return CreatedAtAction(nameof(GetAppointmentById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var appointment = await _context.Set<Appointment>().FindAsync(id);
            if (appointment == null) return NotFound();

            appointment.AppointmentStatus = AppointmentStatus.Cancelled;
            await _context.SaveChangesAsync();

            return Ok();

        }


        
    }
}

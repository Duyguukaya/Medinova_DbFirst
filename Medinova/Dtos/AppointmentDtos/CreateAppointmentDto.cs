using System;

namespace Medinova.Dtos.AppointmentDtos
{
    public class CreateAppointmentDto
    {
        public string FullName { get; set; }
        public int DoctorId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
    }
}
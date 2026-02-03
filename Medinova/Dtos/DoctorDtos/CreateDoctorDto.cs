namespace Medinova.Dtos.DoctorDtos
{
    public class CreateDoctorDto
    {
        public string FullName { get; set; }
        public string Image { get; set; }
        public int DepartmentId { get; set; }
        public string Description { get; set; }
    }
}
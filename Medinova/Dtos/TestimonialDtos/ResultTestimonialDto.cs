namespace Medinova.Dtos.TestimonialDtos
{
    public class ResultTestimonialDto
    {
        public int TestimonialId { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }
    }
}
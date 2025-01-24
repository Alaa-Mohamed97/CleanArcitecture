namespace CleanArcitecture.Core.Features.Students.Queries.DTOs
{
    public class StudentDetailsDto
    {
        public int StudID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string? DepartmentName { get; set; }
    }
}

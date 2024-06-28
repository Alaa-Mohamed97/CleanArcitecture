using CleanArcitecture.Domain.Entities;

namespace CleanArcitecture.Service.Abstracts
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudentsListAsync();
    }
}

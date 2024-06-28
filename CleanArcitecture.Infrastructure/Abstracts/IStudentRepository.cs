using CleanArcitecture.Domain.Entities;

namespace CleanArcitecture.Infrastructure.Abstracts
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();
    }
}

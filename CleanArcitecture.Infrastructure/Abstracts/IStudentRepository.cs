using CleanArcitecture.Domain.Entities;
using CleanArcitecture.Infrastructure.BaseRepositories;

namespace CleanArcitecture.Infrastructure.Abstracts
{
    public interface IStudentRepository : IGenericRepositoryAsync<Student>
    {
        Task<List<Student>> GetStudentsAsync();
    }
}

using CleanArcitecture.Domain.Entities;

namespace CleanArcitecture.Service.Abstracts
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudentsListAsync();
        Task<Student> GetStudentByIdAsync(int studentId);
        Task<string> Create(Student student);
        Task<string> Edit(Student student);
        Task<bool> IsNameExist(string studentName);
        Task<bool> IsNameExistExcludeSelf(string studentName, int id);
        Task<string> Delete(Student student);
    }
}

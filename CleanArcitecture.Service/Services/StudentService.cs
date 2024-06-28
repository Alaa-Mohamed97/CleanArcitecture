using CleanArcitecture.Domain.Entities;
using CleanArcitecture.Infrastructure.Abstracts;
using CleanArcitecture.Service.Abstracts;

namespace CleanArcitecture.Service.Services
{
    public class StudentService : IStudentService
    {
        #region fields
        private readonly IStudentRepository _studentRepository;
        #endregion
        #region Contructors
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        #endregion
        #region Methods
        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _studentRepository.GetStudentsAsync();
        }
        #endregion
    }
}
